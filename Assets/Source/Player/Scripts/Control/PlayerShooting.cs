using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{
    private PlayerControling _playerInput;

    public event UnityAction Shooted;
    public event UnityAction NonShooted;

    private void Awake()
    {
        _playerInput = new PlayerControling();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Shoot.performed += ctx => OnPerformed();
        _playerInput.Player.Shoot.canceled += ctx => Oncanceled();
    }

    private void OnDisable()
    {
        _playerInput.Player.Shoot.performed -= ctx => OnPerformed();
        _playerInput.Player.Shoot.canceled -= ctx => Oncanceled();
        _playerInput.Disable();
    }

    public void ControlShoot(bool isShooted)
    {
        if(isShooted)
            Shooted?.Invoke();
        else
            NonShooted?.Invoke();
    }

    private void OnPerformed()
    {
        Shooted?.Invoke();
    }

    private void Oncanceled()
    {
        NonShooted?.Invoke();
    }
}
