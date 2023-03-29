using UnityEngine;

public class ShowingUpgradeWeapon : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private GameObject _background;
    [SerializeField] private float _delay;
    [SerializeField] private float _delaySelectingWeapon;

    private MenuUpgradingWeapon _menuUpgradingWeapon;

    private void Awake()
    {
        _menuUpgradingWeapon = GetComponentInChildren<MenuUpgradingWeapon>();
    }

    private void OnEnable()
    {
        _background.SetActive(true);
        Invoke(nameof(ShowScore), _delay);
        Invoke(nameof(SelectWeapon), _delaySelectingWeapon);
    }

    private void ShowScore()
    {
        _menuUpgradingWeapon.gameObject.SetActive(true);
    }

    private void SelectWeapon()
    {
        _playerWeaponSelecting.Select(1);
        _playerWeaponSelecting.Select(0);
    }
}

