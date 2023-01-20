using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonBuyingAmmo : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private int _price;
    [SerializeField] private uint _amount;

    public event UnityAction<Weapon.Types, uint> Buyed;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_playerWallet.TryBuy(_price))
            Buyed?.Invoke(_playerWeaponSelecting.CurrentWeapon.Type, _amount);
    }
}
