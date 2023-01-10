using UnityEngine;
using UnityEngine.Events;

public class UpgradingWeapon : MonoBehaviour
{
    [SerializeField] private StandardWeapon[] _standardWeapon;

    private ButtonSelectingWeapon[] _buttonsSelectingWeapons;
    private ButtonSelectingAccessory[] _buttonsSelectingAccessories;

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

    private void OnSelectedWeapon(Weapon.Types type)
    {
        SelectedWeapon?.Invoke(type);
    }
    
    private void OnSelectedAccessory(WeaponAccessories.Type type)
    {
        SelectedAccessory?.Invoke(type);
    }
}
