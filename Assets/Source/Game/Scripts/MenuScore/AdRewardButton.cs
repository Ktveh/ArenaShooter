using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AdRewardButton : MonoBehaviour, IPointerDownHandler
{
    public event UnityAction Down;

    public void OnPointerDown(PointerEventData eventData)
    {
        Down?.Invoke();
    }
}
