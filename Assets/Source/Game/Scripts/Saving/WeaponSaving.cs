using UnityEngine;

[RequireComponent(typeof(Game))]
public class WeaponSaving : MonoBehaviour
{
    private const string True = "True";
    private const string False = "False";
    private const string Ammo = "Ammo";

    [SerializeField] private MenuUpgradingWeapon _upgradingWeapon;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private ButtonBuyingAmmo _buttonBuyingAmmo;
    [SerializeField] private ButtonBuyingGrenade _buttonBuyingGrenade;

    private Game _game;

    private void Awake()
    {
        _game = GetComponent<Game>();
    }

    private void OnEnable()
    {
        _upgradingWeapon.SelectedAccessory += OnSelectedAccessory;
        _playerWeaponSelecting.Selected += OnSelectedWeapon;
        _game.LevelCompleted += OnSelectedWeapon;
        _playerInventory.Changed += OnChanged;
        _buttonBuyingAmmo.Buyed += OnBuyed;
        _buttonBuyingGrenade.Buyed += OnBuyed;
    }

    private void OnDisable()
    {
        _upgradingWeapon.SelectedAccessory -= OnSelectedAccessory;
        _playerWeaponSelecting.Selected -= OnSelectedWeapon;
        _game.LevelCompleted -= OnSelectedWeapon;
        _playerInventory.Changed -= OnChanged;
        _buttonBuyingAmmo.Buyed -= OnBuyed;
        _buttonBuyingGrenade.Buyed -= OnBuyed;
    }

    public bool TryGetAccessory(Weapon.Types weapon, WeaponAccessories.Type accessory)
    {
        return PlayerPrefs.GetString(weapon.ToString() + accessory.ToString()) == True;
    }

    public int GetAmountAmmo (Weapon.Types weapon)
    {
        return PlayerPrefs.GetInt(weapon.ToString() + Ammo);
    }

    private void OnSelectedAccessory(WeaponAccessories.Type type)
    {
        ChangeAccessory(_playerWeaponSelecting.CurrentWeapon.Type.ToString(), type);
    }

    private void ChangeAccessory(string weapon, WeaponAccessories.Type type)
    {
        if (type == WeaponAccessories.Type.Silencer)
        {
            PlayerPrefs.SetString(weapon + WeaponAccessories.Type.Silencer, True);
        }
        else
        {
            PlayerPrefs.SetString(weapon + WeaponAccessories.Type.Scope, False);
            PlayerPrefs.SetString(weapon + WeaponAccessories.Type.Scope1, False);
            PlayerPrefs.SetString(weapon + WeaponAccessories.Type.Scope2, False);
        }

        switch (type)
        {
            case WeaponAccessories.Type.Scope:
                PlayerPrefs.SetString(weapon + WeaponAccessories.Type.Scope, True);
                break;

            case WeaponAccessories.Type.Scope1:
                PlayerPrefs.SetString(weapon + WeaponAccessories.Type.Scope1, True);
                break;

            case WeaponAccessories.Type.Scope2:
                PlayerPrefs.SetString(weapon + WeaponAccessories.Type.Scope2, True);
                break;
        }
    }

    private void OnChanged()
    {
        Weapon.Types[] typesWeapons = _playerInventory.GetTypeAmmo();

        foreach (Weapon.Types weapon in typesWeapons)
            PlayerPrefs.SetInt(weapon + Ammo, (int)_playerInventory.GetAmountAmmo(weapon) + (int)_playerWeaponSelecting.GetWeapon(weapon).CurrentAmountAmmo);

        PlayerPrefs.SetInt(Weapon.Types.Grenade.ToString() + Ammo, (int)_playerInventory.GetAmountAmmo(Weapon.Types.Grenade));
    }

    private void OnSelectedWeapon()
    {
        OnChanged();
    }

    private void OnBuyed(Weapon.Types type, uint amount)
    {
        OnChanged();
    }
}
