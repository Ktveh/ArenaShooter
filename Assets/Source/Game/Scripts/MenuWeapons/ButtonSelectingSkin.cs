using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonBuyingSkin))]
public class ButtonSelectingSkin : MonoBehaviour
{
    private const int NumberKillsForGold = 10000;
    private const int NumberKillsForSilver = 5000;
    private const int NumberKillsForBronze = 1000;

    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private PlayerSaving _playerSaving;
    [SerializeField] private WeaponSkinSaving _weaponSkinSaving;
    [SerializeField] private Button _next;
    [SerializeField] private Button _prev;
    [SerializeField] private Image _preview;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _blocking;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private int[] _prices;

    private ButtonBuyingSkin _buttonBuyingSkin;
    private int _currentNumber;

    private bool _isError => _sprites.Length != _prices.Length;
    private int _numberGoldSkin => _sprites.Length - 1;
    private int _numberSilverSkin => _sprites.Length - 2;
    private int _numberBronzeSkin => _sprites.Length - 3;

    public int NumberLastSkin => _numberGoldSkin;

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

    public void Change(int number = -1)
    {
        if (number != -1)
        {
            _currentNumber = number;
        }

        int price = _prices[_currentNumber];
        bool isCanBuy = true;

        if (_weaponSkinSaving.Check(_playerWeaponSelecting.CurrentWeapon.Type, (WeaponSkin.Names)_currentNumber))
            price = 0;

        _preview.gameObject.SetActive(_currentNumber != 0);
        _preview.sprite = _sprites[_currentNumber];
        _price.text = price.ToString();

        if (_currentNumber == _numberGoldSkin)
            isCanBuy = TryGetPrize(NumberKillsForGold);
        else if(_currentNumber == _numberSilverSkin)
            isCanBuy = TryGetPrize(NumberKillsForSilver);
        else if(_currentNumber == _numberBronzeSkin)
            isCanBuy = TryGetPrize(NumberKillsForBronze);

        _blocking.gameObject.SetActive(isCanBuy ? false : true);

        _buttonBuyingSkin.Get((WeaponSkin.Names)_currentNumber, price, isCanBuy);
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
        Change();
    }

    private bool TryGetPrize(int number)
    {
        return _playerSaving.CurrentScore >= number;
    }
}
