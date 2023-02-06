using UnityEngine;

public class GameControllingPlayer : MonoBehaviour
{
    [SerializeField] private StarterAssets.FirstPersonController _personController;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private PlayerWeaponReloading _playerWeaponReloading;
    [SerializeField] private PlayerShooting _playerShooting;
    [SerializeField] private PlayerScopeOpening _playerScopeOpening;

    private void OnEnable()
    {
        _personController.enabled = true;
        _playerWeaponSelecting.enabled = true;
        _playerWeaponReloading.enabled = true;
        _playerShooting.enabled = true;
        _playerScopeOpening.enabled = true;
    }

    private void OnDisable()
    {
        _personController.enabled = false;
        _playerWeaponSelecting.enabled = false;
        _playerWeaponReloading.enabled = false;
        _playerShooting.enabled = false;
        _playerScopeOpening.enabled = false;
    }

    public void EnableWeaponSelecting()
    {
        _playerWeaponSelecting.enabled = true;
    }
}
