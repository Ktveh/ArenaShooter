using UnityEngine;

public class ShowingUpgradeWeapon : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private GameObject _background;
    [SerializeField] private float _delay;

    private MenuUpgradingWeapon _menuUpgradingWeapon;

    private void Awake()
    {
        _menuUpgradingWeapon = GetComponentInChildren<MenuUpgradingWeapon>();
    }

    private void OnEnable()
    {
        _background.SetActive(true);
        Invoke(nameof(ShowScore), _delay);
    }

    private void ShowScore()
    {
        _menuUpgradingWeapon.gameObject.SetActive(true);
        _playerWeaponSelecting.Select(0);
    }
}

