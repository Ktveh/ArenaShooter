using UnityEngine;
using UnityEngine.UI;

public class ShowingWeaponIcon : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private Image _iconWeapon;

    private void OnEnable()
    {
        _playerWeaponSelecting.Selected += OnSelected;
    }

    private void OnDisable()
    {
        _playerWeaponSelecting.Selected -= OnSelected;
    }

    public void OnSelected()
    {
        if (_playerWeaponSelecting.CurrentWeapon.TryGetComponent(out Weapon weapon))
            if (weapon.Icon != null)
                _iconWeapon.sprite = weapon.Icon;
    }
}
