using UnityEngine;
using UnityEngine.Events;

public class PlayerDroppingGrenade : MonoBehaviour
{
    private PlayerControling _playerInput;

    public event UnityAction Threw;

    private void Awake()
    {
        _playerInput = new PlayerControling();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.ThrowGrenade.performed += ctx => OnPerformed();
    }

    private void OnDisable()
    {
        _playerInput.Player.ThrowGrenade.performed -= ctx => OnPerformed();
        _playerInput.Disable();
    }

    public void ControlShoot(bool isShooted)
    {
        Threw?.Invoke();
    }

    private void OnPerformed()
    {
        Threw?.Invoke();
    }
}
