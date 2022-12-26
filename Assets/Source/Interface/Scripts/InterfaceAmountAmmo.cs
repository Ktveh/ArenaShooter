using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceAmountAmmo : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private TMP_Text _currentAmountAmmo;
    [SerializeField] private TMP_Text _inventoryAmountAmmo;
    [SerializeField] private Image _icon;

    private void OnEnable()
    {
        _playerWeaponSelecting.Selected += OnSelected;
        _playerInventory.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _playerWeaponSelecting.CurrentWeapon.Shooted -= OnShooted;
        _playerWeaponSelecting.Selected -= OnSelected;
        _playerInventory.Changed -= OnChanged;
    }

    private void OnSelected()
    {
        _playerWeaponSelecting.CurrentWeapon.Shooted += OnShooted;
        _playerWeaponSelecting.CurrentWeapon.Reloaded += OnReloaded;

        _icon.sprite = _playerWeaponSelecting.CurrentWeapon.Icon;
        _currentAmountAmmo.text = _playerWeaponSelecting.CurrentWeapon.CurrentAmountAmmo.ToString();
        _inventoryAmountAmmo.text = _playerInventory.GetAmountAmmo(_playerWeaponSelecting.CurrentWeapon.Type).ToString();
    }

    private void OnChanged()
    {
        _inventoryAmountAmmo.text = _playerInventory.GetAmountAmmo(_playerWeaponSelecting.CurrentWeapon.Type).ToString();
    }

    private void OnShooted()
    {
        _currentAmountAmmo.text = _playerWeaponSelecting.CurrentWeapon.CurrentAmountAmmo.ToString();
    }
    
    private void OnReloaded()
    {
        _currentAmountAmmo.text = _playerWeaponSelecting.CurrentWeapon.CurrentAmountAmmo.ToString();
        _inventoryAmountAmmo.text = _playerInventory.GetAmountAmmo(_playerWeaponSelecting.CurrentWeapon.Type).ToString();
    }
}
