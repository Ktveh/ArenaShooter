using UnityEngine;

[RequireComponent(typeof(Game))]
public class PlayerSaving : MonoBehaviour
{
    public const string Money = "Money";
    public const string AmountKilledZombie = "AmountKilledZombie";

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

    //////////////////////////////////////////////////
    public void DeletAllSave()
    {
        PlayerPrefs.DeleteAll();
    }
    //////////////////////////////////////////////////

    public void Recover(string parameter, int value)
    {
        PlayerPrefs.SetInt(parameter, value);

        if(parameter == AmountKilledZombie)
            _playerZombieKillCounter.Recover(value);
        else if(parameter == Money)
            _playerWallet.SetValue(value);
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
