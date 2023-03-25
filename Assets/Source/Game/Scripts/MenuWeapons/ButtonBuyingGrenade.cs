using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonBuyingGrenade : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private int _price;

    public event UnityAction<Weapon.Types, uint> Buyed;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_playerInventory.IsMaxAmountGrenades == false)
        {
            if (_playerWallet.TryBuy(_price))
                Buyed?.Invoke(Weapon.Types.Grenade, 1);
        }
    }
}
