using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonSelectingSkin))]
[RequireComponent(typeof(ChangingColorImage))]
public class ButtonBuyingSkin : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private WeaponSkinSaving _weaponSkinSaving;
    [SerializeField] private Button _buying;

    private WeaponSkin.Names _skin;
    private ButtonSelectingSkin _buttonSelectingSkin;
    private ChangingColorImage _changingColorImage;
    private int _price;

    private void Awake()
    {
        _buttonSelectingSkin = GetComponent<ButtonSelectingSkin>();
        _changingColorImage = GetComponent<ChangingColorImage>();
    }

    private void OnEnable()
    {
        _buying.onClick.AddListener(OnBuying);
    }

    private void OnDisable()
    {
        _buying.onClick.RemoveListener(OnBuying);
    }

    public void Get(WeaponSkin.Names name, int price, bool isCanBuy = true)
    {
        _skin = name;
        _price = price;
        _buying.interactable = isCanBuy;

        if(isCanBuy)
            _changingColorImage.ChangeToDefault();
        else
            _changingColorImage.ChangeToTarget();
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
