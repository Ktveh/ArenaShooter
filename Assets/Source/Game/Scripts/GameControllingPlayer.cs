using UnityEngine;

public class GameControllingPlayer : MonoBehaviour
{
    [SerializeField] private PlayerDirection _playerDirection;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerJumping _playerJumping;
    [SerializeField] private PlayerWeaponReloading _playerWeaponReloading;
    [SerializeField] private PlayerScopeOpening _playerScopeOpening;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerDroppingGrenade _playerDroppingGrenade;
    [SerializeField] private PlayerPausingGame _playerPausingGame;

    private void OnEnable()
    {
        _playerMovement.enabled = false;
        _playerJumping.enabled = false;
        _playerWeaponReloading.enabled = false;
        _playerScopeOpening.enabled = false;
        _playerShooting.enabled = false;
        _playerDroppingGrenade.enabled = false;
    }

    private void OnDisable()
    {
        _playerMovement.enabled = true;
        _playerJumping.enabled = true;
        _playerWeaponReloading.enabled = true;
        _playerScopeOpening.enabled = true;
        _playerShooting.enabled = true;
        _playerDroppingGrenade.enabled = true;
    }
}
