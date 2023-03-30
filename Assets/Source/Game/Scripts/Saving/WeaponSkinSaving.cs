using UnityEngine;

public class WeaponSkinSaving : MonoBehaviour
{
    private const string Skin = "Skin";
    private const string Current = "Current";
    private const string True = "True";

    public bool Check(Weapon.Types weapon, WeaponSkin.Names name)
    {
        return PlayerPrefs.GetString(weapon.ToString() + name.ToString() + Skin) == True;
    }

    public string GetCurrent(Weapon.Types weapon)
    {
        return PlayerPrefs.GetString(weapon.ToString() + Current + Skin);
    }

    public void Save(Weapon.Types weapon, WeaponSkin.Names name)
    {
        PlayerPrefs.SetString(weapon.ToString() + name.ToString() + Skin, True);
    }

    public void Save(string weapon, WeaponSkin.Names name)
    {
        PlayerPrefs.SetString(weapon + name.ToString() + Skin, True);
    }

    public void SaveCurrent(Weapon.Types weapon, WeaponSkin.Names name)
    {
        PlayerPrefs.SetString(weapon.ToString() + Current + Skin, name.ToString());
    }
}
