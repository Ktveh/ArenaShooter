using UnityEngine;
using UnityEngine.Events;

public class PlayerPausingGame : MonoBehaviour
{
    private PlayerControling _playerInput;

    public event UnityAction Paused;

    private void Awake()
    {
        _playerInput = new PlayerControling();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Pause.performed += ctx => OnPerformed();
    }

    private void OnDisable()
    {
        _playerInput.Player.Pause.performed -= ctx => OnPerformed();
        _playerInput.Disable();
    }

    public void OnPerformed()
    {
        Paused?.Invoke();
    }
}
