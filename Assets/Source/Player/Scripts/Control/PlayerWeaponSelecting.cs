using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSelecting : MonoBehaviour
{
    [SerializeField] private Transform _containerWeapon;
    [SerializeField] private ControlButton _buttonNextWeapon; 

    private StandardWeapon[] _standardWeaponsAdding;
    private BonusWeapon[] _bonusWeaponsAdding;
    private Dictionary<Weapon.Types, Weapon> _standardWeapons = new Dictionary<Weapon.Types, Weapon>();
    private Dictionary<Weapon.Types, Weapon> _bonusWeapon = new Dictionary<Weapon.Types, Weapon>();
    private Dictionary<KeyCode, Weapon.Types> _weaponsKeysCodes = new Dictionary<KeyCode, Weapon.Types>();
    private Weapon _currentWeapon;
    private int _firstNumberWeapon = 49;
    private bool _isBonusActivated;

    private void Awake()
    {
        _standardWeaponsAdding = _containerWeapon.GetComponentsInChildren<StandardWeapon>();
        _bonusWeaponsAdding = _containerWeapon.GetComponentsInChildren<BonusWeapon>();

        if(_standardWeaponsAdding == null)
            gameObject.SetActive(false);

        for (int i = 0; i < _standardWeaponsAdding.Length; i++)
        {
            Weapon.Types weaponType = _standardWeaponsAdding[i].Type;

            _standardWeapons.Add(weaponType, _standardWeaponsAdding[i]);
            _standardWeapons[weaponType].gameObject.SetActive(false);
            _weaponsKeysCodes.Add((KeyCode)_firstNumberWeapon++, weaponType);
        }

        foreach (Weapon weapon in _bonusWeaponsAdding)
        {
            _bonusWeapon.Add(weapon.Type, weapon);
            weapon.gameObject.SetActive(false);
        }

        if (_standardWeapons.ContainsKey(Weapon.Types.Pistol))
        {
            _currentWeapon = _standardWeapons[Weapon.Types.Pistol];
            _currentWeapon.gameObject.SetActive(true);
        }
    }

    private void OnGUI()
    {
        if (Event.current.isKey)
            if(_weaponsKeysCodes.ContainsKey(Event.current.keyCode))
                Change(_weaponsKeysCodes[Event.current.keyCode]);
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

    private void Change(Weapon.Types weapon)
    {
        if (_isBonusActivated)
            return;

        if (_currentWeapon.Type == weapon)
            return;

        Weapon newWeapon = _standardWeapons[weapon];
        _currentWeapon.Hide();
        newWeapon.Show();
        _currentWeapon = newWeapon;
    }

    private void OnDown()
    {
        for (int i = 0; i < _standardWeaponsAdding.Length; i++)
        {
            if (_currentWeapon.Type == _standardWeaponsAdding[i].Type)
            {
                if (i == _standardWeaponsAdding.Length - 1)
                    i = 0;
                else
                    i++;

                Change(_standardWeaponsAdding[i].Type);
                return;
            }
        }
    }
}
