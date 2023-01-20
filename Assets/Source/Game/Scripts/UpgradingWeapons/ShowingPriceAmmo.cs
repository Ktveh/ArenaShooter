using TMPro;
using UnityEngine;

public class ShowingPriceAmmo : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _amountAmmo;

    private void OnEnable()
    {
        _playerWeaponSelecting.Selected += OnSelected;
    }

    private void OnDisable()
    {
        _playerWeaponSelecting.Selected -= OnSelected;
    }

    private void OnSelected()
    {
        _price.text = _playerWeaponSelecting.CurrentWeapon.PriceAmmo.ToString();
        _amountAmmo.text = _playerWeaponSelecting.CurrentWeapon.MaxAmountAmmo.ToString();
    }
}
