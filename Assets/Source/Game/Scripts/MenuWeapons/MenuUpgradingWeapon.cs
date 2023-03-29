using UnityEngine;
using UnityEngine.Events;

public class MenuUpgradingWeapon : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private Menu _menu;
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
        _menu.Showed += OnShowed;
        _andexAds.Upgraded += OnRewarded;

        foreach (var button in _buttonsSelectingWeapons)
            button.Down += OnSelectedWeapon;
        
        foreach (var button in _buttonsSelectingAccessories)
            button.Down += OnSelectedAccessory;

        _currentWeapon = _playerWeaponSelecting.CurrentWeapon;
    }

    private void OnDisable()
    {
        _menu.Showed += OnShowed;
        _andexAds.Upgraded += OnRewarded;

        foreach (var button in _buttonsSelectingWeapons)
            button.Down -= OnSelectedWeapon;

        foreach (var button in _buttonsSelectingAccessories)
            button.Down -= OnSelectedAccessory;
    }

    private void OnSelectedWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        SelectedWeapon?.Invoke(weapon.Type);
    }

    private void OnShowed()
    {
        _currentWeapon = _playerWeaponSelecting.CurrentWeapon;
    }

    private void OnRewarded(WeaponAccessories.Type type)
    {
        _isUpgradingOnReward = true;
        OnSelectedAccessory(type);
        _isUpgradingOnReward = false;
    }

    private void OnSelectedAccessory(WeaponAccessories.Type type)
    {
        if (_currentWeapon.TryGetComponent(out WeaponAccessories weaponAccessories))
        {
            switch (type)
            {
                case WeaponAccessories.Type.Scope:
                    SelectedAccessory?.Invoke(type);
                    break;

                case WeaponAccessories.Type.Scope1:
                    Upgrade(weaponAccessories.IsEnabledScope1, weaponAccessories.PriceScope1, type);
                    break;

                case WeaponAccessories.Type.Scope2:
                    Upgrade(weaponAccessories.IsEnabledScope2, weaponAccessories.PriceScope2, type);
                    break;

                case WeaponAccessories.Type.Silencer:
                    Upgrade(weaponAccessories.IsEnabledSilencer, weaponAccessories.PriceSilencer, type);
                    break;
            }

        }
    }

    private void Upgrade(bool isEnable, int price, WeaponAccessories.Type type)
    {
        if (isEnable == false)
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
}
