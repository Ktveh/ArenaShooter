using UnityEngine;

public class GameDisablingPlayerControl : MonoBehaviour
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
        _playerDirection.enabled = false;
        _playerMovement.enabled = false;
        _playerJumping.enabled = false;
        _playerWeaponReloading.enabled = false;
        _playerScopeOpening.enabled = false;
        _playerShooting.enabled = false;
        _playerDroppingGrenade.enabled = false;
        _playerPausingGame.enabled = false;
    }
}
