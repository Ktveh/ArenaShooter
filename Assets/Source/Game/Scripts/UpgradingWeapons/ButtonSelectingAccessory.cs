using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonSelectingAccessory : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private WeaponAccessories.Type _type;

    public event UnityAction<WeaponAccessories.Type> Down;

    public void OnPointerDown(PointerEventData eventData)
    {
        Down?.Invoke(_type);
    }
}
