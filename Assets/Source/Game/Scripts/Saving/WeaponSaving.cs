using UnityEngine;

public class WeaponSaving : MonoBehaviour
{
    private const string True = "True";
    private const string False = "False";

    [SerializeField] private UpgradingWeapon _upgradingWeapon;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;

    private void OnEnable()
    {
        _upgradingWeapon.SelectedAccessory += OnSelectedAccessory;
    }

    private void OnDisable()
    {
        _upgradingWeapon.SelectedAccessory -= OnSelectedAccessory;
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

    private void OnSelectedAccessory(WeaponAccessories.Type type)
    {
        Change(_playerWeaponSelecting.CurrentWeapon.Type.ToString(), type);
    }

    private void Change(string weapon, WeaponAccessories.Type type)
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
}
