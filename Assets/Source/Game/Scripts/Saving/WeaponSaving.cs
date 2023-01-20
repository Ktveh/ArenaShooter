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
        _buttonBuyingAmmo.Buyed += OnBuyed;
    }

    private void OnDisable()
    {
        _upgradingWeapon.SelectedAccessory -= OnSelectedAccessory;
        _playerWeaponSelecting.Selected -= OnSelectedWeapon;
        _game.LevelCompleted -= OnSelectedWeapon;
        _buttonBuyingAmmo.Buyed -= OnBuyed;
    }

    /// ///////////////////////////////////////////////////////////////
    public void DeletAllSave()
    {
        PlayerPrefs.DeleteAll();
    }
    /// //////////////////////////////////////////////////////////////
    

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

    private void OnSelectedWeapon()
    {
        Weapon.Types[] typesWeapons = _playerInventory.GetTypeAmmo();

        foreach (Weapon.Types weapon in typesWeapons)
            PlayerPrefs.SetInt(weapon + Ammo, (int)_playerInventory.GetAmountAmmo(weapon) + (int)_playerWeaponSelecting.GetWeapon(weapon).CurrentAmountAmmo);
    }

    private void OnBuyed(Weapon.Types type, uint amount)
    {
        OnSelectedWeapon();
    }
}
