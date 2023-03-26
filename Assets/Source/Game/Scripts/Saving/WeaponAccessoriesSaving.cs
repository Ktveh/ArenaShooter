using UnityEngine;

public class WeaponAccessoriesSaving : MonoBehaviour
{
    private const string Price = "Price";
    private const string True = "True";

    public bool Check(Weapon.Types weapon, WeaponAccessories.Type accessories)
    {
        return PlayerPrefs.GetString(weapon.ToString() + accessories.ToString() + Price) == True;
    }

    public void Save(Weapon.Types weapon, WeaponAccessories.Type accessories)
    {
        PlayerPrefs.SetString(weapon.ToString() + accessories.ToString() + Price, True);
    }
}
