using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonSelectingWeapon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Weapon _weapon;

    public event UnityAction<Weapon> Down;

    public void OnPointerDown(PointerEventData eventData)
    {
        Down?.Invoke(_weapon);
    }
}
