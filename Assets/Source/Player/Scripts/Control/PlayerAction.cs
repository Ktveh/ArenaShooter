using UnityEngine;
using UnityEngine.Events;

abstract public class PlayerAction : MonoBehaviour
{
    [SerializeField] private ControlButton[] _buttons;
    [SerializeField] private KeyCode _key;

    private int _normalizedKeyMouse;

    public event UnityAction Actioned;

    private void Start()
    {
        switch (_key)
        {
            case KeyCode.Mouse0:
                _normalizedKeyMouse = 0;
                break;

            case KeyCode.Mouse1:
                _normalizedKeyMouse = 1;
                break;
        }
    }

    private void Update()
    {
        if (_key == KeyCode.Mouse0)
        {
            if (Input.GetMouseButton(_normalizedKeyMouse))
                Actioned?.Invoke();
        }
        else if (Input.GetKeyDown(_key) || Input.GetMouseButtonDown(_normalizedKeyMouse))
        { 
            Actioned?.Invoke();
        }
    }

    private void OnEnable()
    {
        if (_buttons == null)
            return;

        foreach (var button in _buttons)
            button.Down += OnDown;
    }

    private void OnDisable()
    {
        if (_buttons == null)
            return;

        foreach (var button in _buttons)
            button.Down -= OnDown;
    }

    private void OnDown()
    {
        Actioned?.Invoke();
        Debug.Log("Pressed " + _key);
    }
}
