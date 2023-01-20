using UnityEngine;
using UnityEngine.Events;

public class MenuUpgradingWeapon : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;

    private ButtonSelectingWeapon[] _buttonsSelectingWeapons;
    private ButtonSelectingAccessory[] _buttonsSelectingAccessories;
    private Weapon _currentWeapon;

    public event UnityAction<Weapon.Types> SelectedWeapon;
    public event UnityAction<WeaponAccessories.Type> SelectedAccessory;

    private void OnEnable()
    {
        _buttonsSelectingWeapons = GetComponentsInChildren<ButtonSelectingWeapon>();
        _buttonsSelectingAccessories = GetComponentsInChildren<ButtonSelectingAccessory>();

        foreach (var button in _buttonsSelectingWeapons)
            button.Down += OnSelectedWeapon;
        
        foreach (var button in _buttonsSelectingAccessories)
            button.Down += OnSelectedAccessory;
    }

    private void OnDisable()
    {
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
    
    private void OnSelectedAccessory(WeaponAccessories.Type type)
    {
        if (_currentWeapon.TryGetComponent(out WeaponAccessories weaponAccessories))
        {
            switch (type)
            {
                case WeaponAccessories.Type.Scope1:
                    if (_playerWallet.TryBuy(weaponAccessories.PriceScope1))
                        SelectedAccessory?.Invoke(type);
                    break;

                case WeaponAccessories.Type.Scope2:
                    if (_playerWallet.TryBuy(weaponAccessories.PriceScope2))
                        SelectedAccessory?.Invoke(type);
                    break;

                case WeaponAccessories.Type.Silencer:
                    if (_playerWallet.TryBuy(weaponAccessories.PriceSilencer))
                        SelectedAccessory?.Invoke(type);
                    break;
            }

        }
    }
}
