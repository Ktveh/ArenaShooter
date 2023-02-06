using UnityEngine;
using UnityEngine.Events;

public class PlayerScopeOpening : MonoBehaviour
{
    private PlayerControling _playerInput;

    public event UnityAction Scoped;
    public event UnityAction NonScoped;

    private void Awake()
    {
        _playerInput = new PlayerControling();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Scope.performed += ctx => OnPerformed();
        _playerInput.Player.Scope.canceled += ctx => OnCanceled();
    }

    private void OnDisable()
    {
        _playerInput.Player.Scope.performed -= ctx => OnPerformed();
        _playerInput.Player.Scope.canceled -= ctx => OnCanceled();
        _playerInput.Disable();
    }

    public void ControlScope(bool isOpened)
    {
        if(isOpened)
            Scoped?.Invoke();
        else
            NonScoped?.Invoke();
    }

    private void OnPerformed()
    {
        Scoped?.Invoke();
    }

    private void OnCanceled()
    {
        NonScoped?.Invoke();
    }
}
