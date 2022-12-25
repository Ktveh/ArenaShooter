using UnityEngine;

public class GettingPlayer : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerScopeOpening _playerScopeOpening;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerDroppingGrenade _playerDroppingGrenade;
    [SerializeField] private PlayerWeaponReloading _playerWeaponReloading;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private Camera _weaponCamera;

    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerScopeOpening PlayerScopeOpening => _playerScopeOpening;
    public PlayerShooting PlayerShooting => _playerShooting;
    public PlayerDroppingGrenade PlayerDroppingGrenade => _playerDroppingGrenade;
    public PlayerWeaponReloading PlayerWeaponReloading => _playerWeaponReloading;
    public PlayerInventory PlayerInventory => _playerInventory;
    public Camera WeaponCamera => _weaponCamera;
}
