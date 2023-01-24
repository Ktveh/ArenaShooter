using UnityEngine;
using UnityEngine.Events;

public class MenuUpgradingWeapon : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private Menu _menu;

    private ButtonSelectingWeapon[] _buttonsSelectingWeapons;
    private ButtonSelectingAccessory[] _buttonsSelectingAccessories;
    private Weapon _currentWeapon;

    public event UnityAction<Weapon.Types> SelectedWeapon;
    public event UnityAction<WeaponAccessories.Type> SelectedAccessory;

    private void OnEnable()
    {
        _buttonsSelectingWeapons = GetComponentsInChildren<ButtonSelectingWeapon>();
        _buttonsSelectingAccessories = GetComponentsInChildren<ButtonSelectingAccessory>();
        _menu.Showed += OnShowed;

        foreach (var button in _buttonsSelectingWeapons)
            button.Down += OnSelectedWeapon;
        
        foreach (var button in _buttonsSelectingAccessories)
            button.Down += OnSelectedAccessory;
    }

    private void OnDisable()
    {
        _menu.Showed += OnShowed;

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
                    if (weaponAccessories.IsEnabledScope1 == false)
                    {
                        if (_playerWallet.TryBuy(weaponAccessories.PriceScope1))
                            SelectedAccessory?.Invoke(type);
                    }
                    break;

                case WeaponAccessories.Type.Scope2:
                    if (weaponAccessories.IsEnabledScope2 == false)
                    {
                        if (_playerWallet.TryBuy(weaponAccessories.PriceScope2))
                            SelectedAccessory?.Invoke(type);
                    }
                    break;

                case WeaponAccessories.Type.Silencer:
                    if (weaponAccessories.IsEnabledSilencer == false)
                    {
                        if (_playerWallet.TryBuy(weaponAccessories.PriceSilencer))
                            SelectedAccessory?.Invoke(type);
                    }
                    break;
            }

        }
    }
}
