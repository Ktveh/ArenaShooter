using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowingWeaponAccessoriesIcon : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private MenuUpgradingWeapon _menuUpgradingWeapon;
    [SerializeField] private Image _iconScope;
    [SerializeField] private Image _iconScope1;
    [SerializeField] private Image _iconScope2;
    [SerializeField] private Image _iconSilencer;
    [SerializeField] private TMP_Text _priceScope1;
    [SerializeField] private TMP_Text _priceScope2;
    [SerializeField] private TMP_Text _priceSilencer;

    private void OnEnable()
    {
        _playerWeaponSelecting.Selected += OnSelected;
        _menuUpgradingWeapon.SelectedAccessory += OnSelectedAccessory;
    }

    private void OnDisable()
    {
        _playerWeaponSelecting.Selected -= OnSelected;
        _menuUpgradingWeapon.SelectedAccessory -= OnSelectedAccessory;
    }

    public void OnSelected()
    {
        if (_playerWeaponSelecting.CurrentWeapon.TryGetComponent(out WeaponAccessories weaponAccessories))
        {
            ShowIcon(weaponAccessories);
            ShowPrice(weaponAccessories);
            ChangeColor(weaponAccessories);
        }
    }

    private void OnSelectedAccessory(WeaponAccessories.Type type)
    {
        OnSelected();
    }

    private void ShowIcon(WeaponAccessories weaponAccessories)
    {
        if (weaponAccessories.IconScope1 != null)
        {
            _iconScope1.sprite = weaponAccessories.IconScope1;
            _iconScope1.gameObject.SetActive(true);
        }
        else
        {
            _iconScope1.gameObject.SetActive(false);
        }

        if (weaponAccessories.IconScope2 != null)
        {
            _iconScope2.sprite = weaponAccessories.IconScope2;
            _iconScope2.gameObject.SetActive(true);
        }
        else
        {
            _iconScope2.gameObject.SetActive(false);
        }

        if (weaponAccessories.IconSilencer != null)
        {
            _iconSilencer.sprite = weaponAccessories.IconSilencer;
            _iconSilencer.gameObject.SetActive(true);
        }
        else
        {
            _iconSilencer.gameObject.SetActive(false);
        }
    }

    private void ShowPrice(WeaponAccessories weaponAccessories)
    {
        _priceScope1.text = weaponAccessories.PriceScope1.ToString();
        _priceScope2.text = weaponAccessories.PriceScope2.ToString();
        _priceSilencer.text = weaponAccessories.PriceSilencer.ToString();
    }

    private void ChangeColor(WeaponAccessories weaponAccessories)
    {
        _iconScope.color = weaponAccessories.IsEnabledScope ? Color.green : Color.white;
        _iconScope1.color = weaponAccessories.IsEnabledScope1 ? Color.green : Color.white;
        _iconScope2.color = weaponAccessories.IsEnabledScope2 ? Color.green : Color.white;
        _iconSilencer.color = weaponAccessories.IsEnabledSilencer ? Color.green : Color.white;
    }
}
