using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;
        [SerializeField] private PlayerScopeOpening _playerScopeOpening;
        [SerializeField] private PlayerShooting _playerShooting;
        [SerializeField] private PlayerWeaponReloading _playerWeaponReloading;
        [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
        [SerializeField] private PlayerDroppingGrenade _playerDroppingGrenade;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }
        
        public void OpenScope(bool virtualScopeState)
        {
            _playerScopeOpening.ControlScope(virtualScopeState);
        }

        public void Shoot(bool virtualShootState)
        {
            _playerShooting.ControlShoot(virtualShootState);
        }
        
        public void Reload(bool virtualReloadState)
        {
            _playerWeaponReloading.ControlReload(virtualReloadState);
        }
        
        public void ThrowGrenade(bool virtualThrowGrenadeState)
        {
            _playerDroppingGrenade.ControlThrow();
        }
    }

}
