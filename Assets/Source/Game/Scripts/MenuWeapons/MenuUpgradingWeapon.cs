using UnityEngine;
using UnityEngine.Events;

public class MenuUpgradingWeapon : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private ShowingScore _menu;
    [SerializeField] private YandexAds _andexAds;

    private ButtonSelectingWeapon[] _buttonsSelectingWeapons;
    private ButtonSelectingAccessory[] _buttonsSelectingAccessories;
    private Weapon _currentWeapon;
    private bool _isUpgradingOnReward;

    public event UnityAction<Weapon.Types> SelectedWeapon;
    public event UnityAction<WeaponAccessories.Type> SelectedAccessory;

    private void OnEnable()
    {
        _buttonsSelectingWeapons = GetComponentsInChildren<ButtonSelectingWeapon>();
        _buttonsSelectingAccessories = GetComponentsInChildren<ButtonSelectingAccessory>();
        _playerWeaponSelecting.Selected += OnSelectedWeapon;
        _andexAds.Upgraded += OnRewarded;

        foreach (var button in _buttonsSelectingWeapons)
            button.Down += OnDownButtonSelecteWeapon;
        
        foreach (var button in _buttonsSelectingAccessories)
            button.Down += OnDownButtonSelectedAccessory;
    }

    private void OnDisable()
    {
        _andexAds.Upgraded -= OnRewarded;
        _playerWeaponSelecting.Selected -= OnSelectedWeapon;

        foreach (var button in _buttonsSelectingWeapons)
            button.Down -= OnDownButtonSelecteWeapon;

        foreach (var button in _buttonsSelectingAccessories)
            button.Down -= OnDownButtonSelectedAccessory;
    }

    private void OnDownButtonSelecteWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        SelectedWeapon?.Invoke(weapon.Type);
    }

    private void OnSelectedWeapon()
    {
        _currentWeapon = _playerWeaponSelecting.CurrentWeapon;
        SelectedWeapon?.Invoke(_playerWeaponSelecting.CurrentWeapon.Type);
    }

    private void OnRewarded(WeaponAccessories.Type type)
    {
        _isUpgradingOnReward = true;
        OnDownButtonSelectedAccessory(type);
        _isUpgradingOnReward = false;
    }

    private void OnDownButtonSelectedAccessory(WeaponAccessories.Type type)
    {
        if (_currentWeapon.TryGetComponent(out WeaponAccessories weaponAccessories))
        {
            switch (type)
            {
                case WeaponAccessories.Type.Scope:
                    SelectedAccessory?.Invoke(type);
                    break;

                case WeaponAccessories.Type.Scope1:
                    Upgrade(weaponAccessories.PriceScope1, type);
                    break;

                case WeaponAccessories.Type.Scope2:
                    Upgrade(weaponAccessories.PriceScope2, type);
                    break;

                case WeaponAccessories.Type.Silencer:
                    Upgrade(weaponAccessories.PriceSilencer, type);
                    break;
            }

        }
    }

    private void Upgrade(int price, WeaponAccessories.Type type)
    {
        if (_isUpgradingOnReward)
        {
            SelectedAccessory?.Invoke(type);
        }
        else
        {
            if (_playerWallet.TryBuy(price))
                SelectedAccessory?.Invoke(type);
        }
    }
}
