using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonSelectingSkin))]
public class ButtonBuyingSkin : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private WeaponSkinSaving _weaponSkinSaving;
    [SerializeField] private Button _buying;

    private WeaponSkin.Names _skin;
    private ButtonSelectingSkin _buttonSelectingSkin;
    private int _price;

    private void Awake()
    {
        _buttonSelectingSkin = GetComponent<ButtonSelectingSkin>();
    }

    private void OnEnable()
    {
        _buying.onClick.AddListener(OnBuying);
    }

    private void OnDisable()
    {
        _buying.onClick.RemoveListener(OnBuying);
    }

    public void Get(WeaponSkin.Names name, int price)
    {
        _skin = name;
        _price = price;
    }

    private void OnBuying()
    {
        if (_playerWallet.TryBuy(_price))
        {
            _weaponSkinSaving.Save(_playerWeaponSelecting.CurrentWeapon.Type, _skin);
            _weaponSkinSaving.SaveCurrent(_playerWeaponSelecting.CurrentWeapon.Type, _skin);
            _buttonSelectingSkin.Change();
        }
    }
}
