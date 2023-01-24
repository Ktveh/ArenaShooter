using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonBuyingAmmo : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;

    public event UnityAction<Weapon.Types, uint> Buyed;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_playerWallet.TryBuy(_playerWeaponSelecting.CurrentWeapon.PriceAmmo))
            Buyed?.Invoke(_playerWeaponSelecting.CurrentWeapon.Type, _playerWeaponSelecting.CurrentWeapon.MaxAmountAmmo);
    }
}
