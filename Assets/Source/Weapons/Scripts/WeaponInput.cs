using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    [SerializeField] private StarterAssets.FirstPersonController _personController;
    [SerializeField] private PlayerScopeOpening _playerScopeOpening;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerDroppingGrenade _playerDroppingGrenade;
    [SerializeField] private PlayerWeaponReloading _playerWeaponReloading;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private Camera _weaponCamera;
    [SerializeField] private WeaponSaving _weaponSaving;
    [SerializeField] private WeaponAccessoriesSaving _weaponAccessoriesSaving;
    [SerializeField] private WeaponSkinSaving _weaponSkinSaving;
    [SerializeField] private Game _game;

    public StarterAssets.FirstPersonController PersonController => _personController;
    public PlayerScopeOpening PlayerScopeOpening => _playerScopeOpening;
    public PlayerShooting PlayerShooting => _playerShooting;
    public PlayerDroppingGrenade PlayerDroppingGrenade => _playerDroppingGrenade;
    public PlayerWeaponReloading PlayerWeaponReloading => _playerWeaponReloading;
    public PlayerInventory PlayerInventory => _playerInventory;
    public PlayerWeaponSelecting PlayerWeaponSelecting => _playerWeaponSelecting;
    public Camera WeaponCamera => _weaponCamera;
    public WeaponSaving WeaponSaving => _weaponSaving;
    public WeaponAccessoriesSaving WeaponAccessoriesSaving => _weaponAccessoriesSaving;
    public WeaponSkinSaving WeaponSkinSaving => _weaponSkinSaving;
    public Game Game => _game;
}

