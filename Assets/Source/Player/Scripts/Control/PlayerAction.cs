using UnityEngine;
using UnityEngine.Events;

abstract public class PlayerAction : MonoBehaviour
{
    [SerializeField] private ControlButton[] _buttons;
    [SerializeField] private KeyCode _key;

    private int _normalizedKeyMouse;

    private bool _isMouse => (_key == KeyCode.Mouse0) || (_key == KeyCode.Mouse1);

    public event UnityAction Actioned;
    public event UnityAction WithoutActioned;

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
        if (Input.GetKeyDown(_key) || (Input.GetMouseButtonDown(_normalizedKeyMouse) && _isMouse))
            Actioned?.Invoke();

        if (Input.GetKeyUp(_key) || (Input.GetMouseButtonUp(_normalizedKeyMouse) && _isMouse))
            WithoutActioned?.Invoke();
    }

    private void OnEnable()
    {
        if (_buttons == null)
            return;

        foreach (var button in _buttons)
        {
            button.Down += OnDown;
            button.Up += OnUp;
        }
    }

    private void OnDisable()
    {
        if (_buttons == null)
            return;

        WithoutActioned?.Invoke();

        foreach (var button in _buttons)
        {
            button.Down -= OnDown;
            button.Up -= OnUp;
        }
    }

    private void OnDown()
    {
        Actioned?.Invoke();
    }

    private void OnUp()
    {
        WithoutActioned?.Invoke();
    }
}
