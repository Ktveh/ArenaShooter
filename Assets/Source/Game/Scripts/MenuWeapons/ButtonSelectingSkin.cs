using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonBuyingSkin))]
public class ButtonSelectingSkin : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private WeaponSkinSaving _weaponSkinSaving;
    [SerializeField] private Button _next;
    [SerializeField] private Button _prev;
    [SerializeField] private Image _preview;
    [SerializeField] private TMP_Text _price; 
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private int[] _prices;

    private ButtonBuyingSkin _buttonBuyingSkin;
    private int _currentNumber;

    private bool _isError => _sprites.Length != _prices.Length;

    private void Awake()
    {
        _buttonBuyingSkin = GetComponent<ButtonBuyingSkin>();
    }

    private void Start()
    {
        if (_isError)
        {
            Debug.LogError("Arrays of different lengths");
            return;
        }
    }

    private void OnEnable()
    {
        _next.onClick.AddListener(OnNext);
        _prev.onClick.AddListener(OnPrev);
        _playerWeaponSelecting.Selected += OnSelected;
    }

    private void OnDisable()
    {
        _next.onClick.RemoveListener(OnNext);
        _prev.onClick.RemoveListener(OnPrev);
        _playerWeaponSelecting.Selected -= OnSelected;
    }

    public void Change()
    {
        int price = _prices[_currentNumber];

        if (_weaponSkinSaving.Check(_playerWeaponSelecting.CurrentWeapon.Type, (WeaponSkin.Names)_currentNumber))
            price = 0;

        _preview.sprite = _sprites[_currentNumber];
        _price.text = price.ToString();

        _buttonBuyingSkin.Get((WeaponSkin.Names)_currentNumber, price);
    }

    private void OnNext()
    {
        if (_isError)
            return;

        if (_currentNumber == _sprites.Length - 1)
            _currentNumber = 0; 
        else
            ++_currentNumber;
        
        Change();
    }

    private void OnPrev()
    {
        if (_isError)
            return;

        if (_currentNumber == 0)
            _currentNumber = _sprites.Length - 1;
        else
            --_currentNumber;

        Change();
    }

    private void OnSelected()
    {
        _currentNumber = 0;
        _preview.sprite = _sprites[_currentNumber];
        _price.text = _prices[_currentNumber].ToString();
    }
}
