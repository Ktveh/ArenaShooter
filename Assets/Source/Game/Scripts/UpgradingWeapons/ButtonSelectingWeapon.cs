using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonSelectingWeapon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Weapon.Types _type;

    public event UnityAction<Weapon.Types> Down;

    public void OnPointerDown(PointerEventData eventData)
    {
        Down?.Invoke(_type);
    }
}
