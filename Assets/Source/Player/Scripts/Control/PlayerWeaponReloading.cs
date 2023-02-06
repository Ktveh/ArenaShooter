using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponReloading : MonoBehaviour
{
    private PlayerControling _playerInput;

    public event UnityAction Reloaded;

    private void Awake()
    {
        _playerInput = new PlayerControling();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Reload.performed += ctx => OnPerformed();
    }

    private void OnDisable()
    {
        _playerInput.Player.Reload.performed -= ctx => OnPerformed();
        _playerInput.Disable();
    }

    public void ControlReload(bool isReloaded)
    {
        if(isReloaded)
            Reloaded?.Invoke();
    }

    private void OnPerformed()
    {
        Reloaded?.Invoke();
    }
}
