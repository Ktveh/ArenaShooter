using UnityEngine;

public class WeaponAccessoriesSaving : MonoBehaviour
{
    private const string IsInStock = "IsInStock";
    private const string True = "True";

    public bool Check(Weapon.Types weapon, WeaponAccessories.Type accessories)
    {
        return PlayerPrefs.GetString(weapon.ToString() + accessories.ToString() + IsInStock) == True;
    }

    public void Save(Weapon.Types weapon, WeaponAccessories.Type accessories)
    {
        PlayerPrefs.SetString(weapon.ToString() + accessories.ToString() + IsInStock, True);
    }

    public void Save(string weapon, WeaponAccessories.Type accessories)
    {
        PlayerPrefs.SetString(weapon + accessories.ToString() + IsInStock, True);
    }
}
