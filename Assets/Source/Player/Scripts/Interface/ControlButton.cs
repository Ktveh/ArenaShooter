using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Action _action;

    private bool _isPressed;

    public event UnityAction Down;
    public event UnityAction Up;

    private enum Action
    {
        Default,
        Shot
    }

    private void Update()
    {
        if ((_action == Action.Shot) && _isPressed)
            Down?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        if (_action == Action.Default)
            Down?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        Up?.Invoke();
    }
}
