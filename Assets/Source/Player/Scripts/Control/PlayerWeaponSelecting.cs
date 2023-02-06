using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerWeaponSelecting : MonoBehaviour
{
    [SerializeField] private Transform _containerWeapon;
    [SerializeField] private MenuUpgradingWeapon _upgradingWeapon;

    private StandardWeapon[] _standardWeaponsAdding;
    private BonusWeapon[] _bonusWeaponsAdding;
    private Dictionary<Weapon.Types, StandardWeapon> _standardWeapons = new Dictionary<Weapon.Types, StandardWeapon>();
    private Dictionary<Weapon.Types, BonusWeapon> _bonusWeapon = new Dictionary<Weapon.Types, BonusWeapon>();
    private Dictionary<KeyCode, Weapon.Types> _weaponsKeysCodes = new Dictionary<KeyCode, Weapon.Types>();
    private Weapon _currentWeapon;
    private int _firstNumberWeapon = 49;
    private bool _isBonusActivated;

    public Weapon CurrentWeapon => _currentWeapon;
    public Weapon LastWeapon { get; private set; }

    public event UnityAction Selected;

    private void Awake()
    {
        _standardWeaponsAdding = _containerWeapon.GetComponentsInChildren<StandardWeapon>();
        _bonusWeaponsAdding = _containerWeapon.GetComponentsInChildren<BonusWeapon>();

        if (_standardWeaponsAdding == null)
            gameObject.SetActive(false);

        for (int i = 0; i < _standardWeaponsAdding.Length; i++)
        {
            Weapon.Types weaponType = _standardWeaponsAdding[i].Type;

            _standardWeapons.Add(weaponType, _standardWeaponsAdding[i]);
            _standardWeapons[weaponType].gameObject.SetActive(false);
            _weaponsKeysCodes.Add((KeyCode)_firstNumberWeapon++, weaponType);
        }

        foreach (BonusWeapon weapon in _bonusWeaponsAdding)
        {
            _bonusWeapon.Add(weapon.Type, weapon);
            weapon.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (_standardWeapons.ContainsKey(Weapon.Types.Pistol))
        {
            _currentWeapon = _standardWeapons[Weapon.Types.Pistol];
            _currentWeapon.gameObject.SetActive(true);
            Selected?.Invoke();
        }
    }

    private void OnGUI()
    {
        if (Event.current.isKey)
            if (_weaponsKeysCodes.ContainsKey(Event.current.keyCode))
                Change(_weaponsKeysCodes[Event.current.keyCode]);
    }

    private void OnEnable()
    {
        _upgradingWeapon.SelectedWeapon += Change;
    }

    private void OnDisable()
    {
        _upgradingWeapon.SelectedWeapon -= Change;
    }

    public Weapon GetWeapon(Weapon.Types type)
    {
        return _standardWeapons[type];
    }

    public void Select(int number)
    {
        switch (number)
        {
            case 0:
                Change(Weapon.Types.Pistol);
                break;
            case 1:
                Change(Weapon.Types.SMG);
                break;
            case 2:
                Change(Weapon.Types.Rifle);
                break;
            case 3:
                Change(Weapon.Types.Shotgun);
                break;
            case 4:
                Change(Weapon.Types.SniperRifle);
                break;
        }
    }

    private void Change(Weapon.Types typeWeapon)
    {
        if (_isBonusActivated)
            return;

        if (_currentWeapon.Type == typeWeapon)
            return;

        if (_currentWeapon.IsReloading)
            return;

        LastWeapon = _currentWeapon;
        _currentWeapon.gameObject.SetActive(false);
        StandardWeapon newWeapon = _standardWeapons[typeWeapon];
        newWeapon.gameObject.SetActive(true);
        _currentWeapon = newWeapon;
        Selected?.Invoke();
    }
}