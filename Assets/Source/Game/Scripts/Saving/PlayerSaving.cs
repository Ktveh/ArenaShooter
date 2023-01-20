using UnityEngine;

public class PlayerSaving : MonoBehaviour
{
    private const string Money = "Money";
    private const string AmountKilledZombie = "AmountKilledZombie";

    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerZombieKillCounter _playerZombieKillCounter;

    private void Awake()
    {
        _playerWallet.SetValue(PlayerPrefs.GetInt(Money));
    }

    private void OnEnable()
    {
        _playerWallet.ChangedValue += OnChangedValue;
        _playerZombieKillCounter.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _playerWallet.ChangedValue -= OnChangedValue;
        _playerZombieKillCounter.Changed -= OnChanged;
    }

    private void OnChangedValue(int value)
    {
        PlayerPrefs.SetInt(Money, value);
    }
    
    private void OnChanged(int value)
    {
        PlayerPrefs.SetInt(AmountKilledZombie, value);
    }
}
