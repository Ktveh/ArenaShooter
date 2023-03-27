using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonUpgradingOnReward : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private WeaponAccessories.Type _type;

    public WeaponAccessories.Type TypeAccessory => _type;

    public event UnityAction<WeaponAccessories.Type> Down;

    public void OnPointerDown(PointerEventData eventData)
    {
        Down?.Invoke(_type);
    }
}
