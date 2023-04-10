using Agava.YandexGames;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class SavingToCloud : MonoBehaviour
{
    private const string Levels = "Levels";

    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerZombieKillCounter _playerZombieKillCounter;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private Weapon[] _weapons;
    [SerializeField] private WeaponSkinSaving _weaponSkinSaving;
    [SerializeField] private WeaponAccessoriesSaving _weaponAccessoriesSaving;

    private int _amountKilledZombie;

    public bool IsSuccess { get; private set; }

    public void OnEnable()
    {
        List<CloudSaving> weapons = new List<CloudSaving>();

        if (_playerZombieKillCounter.Count == 0)
            _amountKilledZombie = PlayerPrefs.GetInt(PlayerSaving.AmountKilledZombie);
        else
            _amountKilledZombie = _playerZombieKillCounter.Count;

        foreach (var weapon in _weapons)
        {
            CloudSaving newWeapon = new CloudSaving();

            newWeapon.AmountKilledZombie = _amountKilledZombie;
            newWeapon.Money = _playerWallet.Value;
            newWeapon.CurrentLevel = PlayerPrefs.GetInt(Levels);
            newWeapon.AmountGrenade = (int)_playerInventory.GetAmountAmmo(Weapon.Types.Grenade); ;
            newWeapon.AmountAmmo = (int)_playerInventory.GetAmountAmmo(weapon.Type);
            newWeapon.TypeWeapon = weapon.Type.ToString();
            newWeapon.IsScope1InStock = _weaponAccessoriesSaving.Check(weapon.Type, WeaponAccessories.Type.Scope1);
            newWeapon.IsScope2InStock = _weaponAccessoriesSaving.Check(weapon.Type, WeaponAccessories.Type.Scope2);
            newWeapon.IsSilencerInStock = _weaponAccessoriesSaving.Check(weapon.Type, WeaponAccessories.Type.Silencer);
            newWeapon.IsSkinInStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin);
            newWeapon.IsSkin1InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin1);
            newWeapon.IsSkin2InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin2);
            newWeapon.IsSkin3InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin3);
            newWeapon.IsSkin4InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin4);
            newWeapon.IsSkin5InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin5);
            newWeapon.IsSkin6InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin6);
            newWeapon.IsSkin7InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin7);
            newWeapon.IsSkin8InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin8);
            newWeapon.IsSkin9InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin9);
            newWeapon.IsSkin10InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin10);
            newWeapon.IsSkin11InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin11);
            newWeapon.IsSkin12InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin12);
            newWeapon.IsSkin13InStok = _weaponSkinSaving.Check(weapon.Type, WeaponSkin.Names.Skin13);

            weapons.Add(newWeapon);
        }

        string jsonData = JsonConvert.SerializeObject(weapons, Formatting.Indented);

#if !UNITY_WEBGL || UNITY_EDITOR
        IsSuccess = true;
        return;
#endif

        PlayerAccount.SetPlayerData(jsonData);
        IsSuccess = true;
    }
}
