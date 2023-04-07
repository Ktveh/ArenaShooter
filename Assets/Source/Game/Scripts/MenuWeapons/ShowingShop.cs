using UnityEngine;

public class ShowingShop : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private ButtonSelectingSkin _buttonSelectingSkin;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private float _delayClosingLoadingScreen;
    [SerializeField] private float _delaySelectingWeapon;
    [SerializeField] private float _delaySelectingSkin;

    private MenuUpgradingWeapon _menuUpgradingWeapon;

    private void Awake()
    {
        _menuUpgradingWeapon = GetComponentInChildren<MenuUpgradingWeapon>();

        _menuUpgradingWeapon.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _loadingScreen.SetActive(true);
        _background.SetActive(true);
        _menuUpgradingWeapon.gameObject.SetActive(true);
        Invoke(nameof(CloseLoadingScreen), _delayClosingLoadingScreen);
        Invoke(nameof(SelectWeapon), _delaySelectingWeapon);
        Invoke(nameof(SelectLastSkin), _delaySelectingSkin);
        Invoke(nameof(SelectDefaultSkin), _delaySelectingSkin + _delaySelectingSkin);
    }

    private void CloseLoadingScreen()
    {
        _loadingScreen.SetActive(false);
    }

    private void SelectWeapon()
    {
        _playerWeaponSelecting.Select(1);
        _playerWeaponSelecting.Select(0);
    }

    private void SelectLastSkin()
    {
        _buttonSelectingSkin.Change(_buttonSelectingSkin.NumberLastSkin);
    }

    private void SelectDefaultSkin()
    {
        _buttonSelectingSkin.Change(0);
    }
}

