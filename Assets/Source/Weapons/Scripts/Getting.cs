using UnityEngine;

public class Getting : MonoBehaviour
{
    [SerializeField] private StarterAssets.FirstPersonController _personController;
    [SerializeField] private PlayerScopeOpening _playerScopeOpening;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerDroppingGrenade _playerDroppingGrenade;
    [SerializeField] private PlayerWeaponReloading _playerWeaponReloading;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private Camera _weaponCamera;
    [SerializeField] private WeaponSaving _weaponSaving;

    public StarterAssets.FirstPersonController PersonController => _personController;
    public PlayerScopeOpening PlayerScopeOpening => _playerScopeOpening;
    public PlayerShooting PlayerShooting => _playerShooting;
    public PlayerDroppingGrenade PlayerDroppingGrenade => _playerDroppingGrenade;
    public PlayerWeaponReloading PlayerWeaponReloading => _playerWeaponReloading;
    public PlayerInventory PlayerInventory => _playerInventory;
    public Camera WeaponCamera => _weaponCamera;
    public WeaponSaving WeaponSaving => _weaponSaving;
}

