using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private ShowingScore _menu;
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private YandexAds _yandexAds;
    [SerializeField] private VKAds _vkAds;
    [SerializeField] private CrazyAdvertising _crazyAdvertising;
    [SerializeField] private VKInviting _vkInviting;

    private int _value;

    public int Value => _value;

    public event UnityAction<int> ChangedValue;

    private void OnEnable()
    {
        _menu.Showed += OnShowed;
        _yandexAds.GetedGold += OnGetedGold;
        _vkAds.GetedGold += OnGetedGold;
        _crazyAdvertising.GetedGold += OnGetedGold;

        if(_vkInviting != null)
            _vkInviting.Rewarded += OnRewarded;
    }

    private void OnDisable()
    {
        _menu.Showed -= OnShowed;
        _yandexAds.GetedGold -= OnGetedGold;
        _vkAds.GetedGold -= OnGetedGold;
        _crazyAdvertising.GetedGold -= OnGetedGold;

        if (_vkInviting != null)
            _vkInviting.Rewarded -= OnRewarded;
    }

    public void SetValue(int value)
    {
        _value = value;
    }
    
    public bool TryBuy(int price)
    {
        if (_value >= price)
        {
            _value -= price;
            ChangedValue?.Invoke(_value);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnShowed()
    {
        _value += _levelReward.AllReward;
        ChangedValue.Invoke(_value);
    }

    private void OnGetedGold()
    {
        _value += _levelReward.AllReward;
        ChangedValue.Invoke(_value);
    }

    private void OnRewarded(int money)
    {
        _value += money;
        ChangedValue.Invoke(_value);
    }
}
