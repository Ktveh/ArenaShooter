using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private LevelReward _levelReward;

    private int _value;

    public int Value => _value;

    public event UnityAction<int> ChangedValue;

    private void OnEnable()
    {
        _menu.Showed += OnShowed;
    }

    private void OnDisable()
    {
        _menu.Showed -= OnShowed;
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
}
