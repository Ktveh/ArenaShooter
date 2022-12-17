using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform _containerWeapon;
    [SerializeField] private ControlButton _buttonNextWeapon; 

    private Weapon[] _weaponsAdded;
    private Dictionary<Weapon.Types, Weapon> _weapons = new Dictionary<Weapon.Types, Weapon>();
    private Weapon _currentWeapon;
    private bool _isBonusActivated;

    private void Awake()
    {
        _weaponsAdded = _containerWeapon.GetComponentsInChildren<Weapon>();

        if(_weaponsAdded == null)
            gameObject.SetActive(false);

        foreach (Weapon weapon in _weaponsAdded)
        {
            _weapons.Add(weapon.Type, weapon);
            weapon.gameObject.SetActive(false);
        }

        if (_weapons.ContainsKey(Weapon.Types.Pistol) && (_weapons[Weapon.Types.Pistol] != null))
        {
            _currentWeapon = _weapons[Weapon.Types.Pistol];
            _currentWeapon.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Change(Weapon.Types.Pistol);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Change(Weapon.Types.SMG);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            Change(Weapon.Types.Rifle);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            Change(Weapon.Types.SniperRifle);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            Change(Weapon.Types.Shotgun);
    }

    private void OnEnable()
    {
        //eventActivatedBonus. += Change;
        //eventNonActivatedBonus. += Change;
        _buttonNextWeapon.Down += OnDown;
    }

    private void OnDisable()
    {
        //eventActivatedBonus. -= Change;
        //eventNonActivatedBonus. -= Change;
        _buttonNextWeapon.Down -= OnDown;
    }

    private void Change(Weapon.Types newing)
    {
        if (_isBonusActivated)
            return;

        if (_currentWeapon.Type == newing)
            return;

        Weapon newWeapon = _weapons[newing];
        _currentWeapon.Hide();
        newWeapon.Show();
        _currentWeapon = newWeapon;
    }

    private void OnDown()
    {
        for (int i = 0; i < _weaponsAdded.Length; i++)
        {
            if (_currentWeapon.Type == _weaponsAdded[i].Type)
            {
                if(_weaponsAdded[i + 1].Type == Weapon.Types.MiniGun)
                    i = 0;
                else
                    ++i;
                
                Change(_weaponsAdded[i].Type);
                break;
            }
        }
    }
}
