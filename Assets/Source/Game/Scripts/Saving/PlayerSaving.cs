using UnityEngine;

[RequireComponent(typeof(Game))]
public class PlayerSaving : MonoBehaviour
{
    private const string Money = "Money";
    private const string AmountKilledZombie = "AmountKilledZombie";

    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private PlayerZombieKillCounter _playerZombieKillCounter;

    private Game _game;

    public int CurrentScore => PlayerPrefs.GetInt(AmountKilledZombie);

    private void Awake()
    {
        _game = GetComponent<Game>();
        _playerWallet.SetValue(PlayerPrefs.GetInt(Money));
    }

    private void OnEnable()
    {
        _playerWallet.ChangedValue += OnChangedValue;
        _game.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        _playerWallet.ChangedValue -= OnChangedValue;
        _game.LevelCompleted -= OnLevelCompleted;
    }

    private void OnChangedValue(int value)
    {
        PlayerPrefs.SetInt(Money, value);
    }
    
    private void OnLevelCompleted()
    {
        int amount = PlayerPrefs.GetInt(AmountKilledZombie) + _playerZombieKillCounter.Count;
        PlayerPrefs.SetInt(AmountKilledZombie, amount);
    }
}
