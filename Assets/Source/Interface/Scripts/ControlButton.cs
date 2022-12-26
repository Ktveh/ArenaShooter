using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event UnityAction Down;
    public event UnityAction Up;

    public void OnPointerDown(PointerEventData eventData)
    {   
        Down?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Up?.Invoke();
    }
}
