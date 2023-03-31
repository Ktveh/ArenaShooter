using UnityEngine;

[RequireComponent(typeof(PlayerSaving))]
[RequireComponent(typeof(WeaponSaving))]
[RequireComponent(typeof(WeaponAccessoriesSaving))]
[RequireComponent(typeof(WeaponSkinSaving))]
public class RestoringLocalSave : MonoBehaviour
{
    private const string Levels = "Levels";

    [SerializeField] private GettingCloudSaving _gettingCloudSaving;

    private PlayerSaving _playerSaving;
    private WeaponSaving _weaponSaving;
    private WeaponAccessoriesSaving _weaponAccessoriesSaving;
    private WeaponSkinSaving _weaponSkinSaving;
    private CloudSaving[] _cloudSavings;

    private void Awake()
    {
        _playerSaving = GetComponent<PlayerSaving>();
        _weaponSaving = GetComponent<WeaponSaving>();
        _weaponAccessoriesSaving = GetComponent<WeaponAccessoriesSaving>();
        _weaponSkinSaving = GetComponent<WeaponSkinSaving>();
    }

    private void OnEnable()
    {
        if (_gettingCloudSaving.Try(out _cloudSavings))
        {
            SetPlayer();
            SetWeapon();
        }
    }

    private void SetPlayer()
    {
        _playerSaving.Recover(PlayerSaving.Money, _cloudSavings[0].Money);
        _playerSaving.Recover(PlayerSaving.AmountKilledZombie, _cloudSavings[0].AmountKilledZombie);
        _weaponSaving.Recover(Weapon.Types.Grenade.ToString(), _cloudSavings[0].AmountGrenade);
        PlayerPrefs.SetInt(Levels, _cloudSavings[0].CurrentLevel);
    }

    private void SetWeapon()
    {
        foreach (var saving in _cloudSavings)
        {
            _weaponSaving.Recover(saving.TypeWeapon, saving.AmountAmmo);

            if(saving.IsScope1InStock)
                _weaponAccessoriesSaving.Save(saving.TypeWeapon, WeaponAccessories.Type.Scope1);

            if(saving.IsScope2InStock)
                _weaponAccessoriesSaving.Save(saving.TypeWeapon, WeaponAccessories.Type.Scope2);

            if (saving.IsSilencerInStock)
                _weaponAccessoriesSaving.Save(saving.TypeWeapon, WeaponAccessories.Type.Silencer);

            if(saving.IsSkinInStok)
                _weaponSkinSaving.Save(saving.TypeWeapon, WeaponSkin.Names.Skin);

            if(saving.IsSkin1InStok)
                _weaponSkinSaving.Save(saving.TypeWeapon, WeaponSkin.Names.Skin1);

            if (saving.IsSkin2InStok)
                _weaponSkinSaving.Save(saving.TypeWeapon, WeaponSkin.Names.Skin2);

            if (saving.IsSkin3InStok)
                _weaponSkinSaving.Save(saving.TypeWeapon, WeaponSkin.Names.Skin3);

            if (saving.IsSkin4InStok)
                _weaponSkinSaving.Save(saving.TypeWeapon, WeaponSkin.Names.Skin4);

            if (saving.IsSkin5InStok)
                _weaponSkinSaving.Save(saving.TypeWeapon, WeaponSkin.Names.Skin5);

            if (saving.IsSkin6InStok)
                _weaponSkinSaving.Save(saving.TypeWeapon, WeaponSkin.Names.Skin6);

            if (saving.IsSkin7InStok)
                _weaponSkinSaving.Save(saving.TypeWeapon, WeaponSkin.Names.Skin7);

            if (saving.IsSkin8InStok)
                _weaponSkinSaving.Save(saving.TypeWeapon, WeaponSkin.Names.Skin8);

        }
    }
}
