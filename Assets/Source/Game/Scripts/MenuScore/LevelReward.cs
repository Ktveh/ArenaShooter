using UnityEngine;

[RequireComponent(typeof(LevelScore))]
public class LevelReward : MonoBehaviour
{
    private const int Multiplier = 2;

    [SerializeField] private int _multiplier;

    private Game _game;
    private LevelScore _levelScore;

    public int KilledZombie
    {
        get
        {
            if(_game.PlayerIsDead)
                return _levelScore.AmountKilledZombie * _multiplier != 0 ? _levelScore.AmountKilledZombie * _multiplier / Multiplier : 0;
            else
                return _levelScore.AmountKilledZombie * _multiplier;
        }

        private set { }
    }
    public int HitedHead
    {
        get
        {
            if (_game.PlayerIsDead)
                return _levelScore.AmountHitedHead * _multiplier != 0 ? _levelScore.AmountHitedHead * _multiplier / Multiplier : 0;
            else
                return _levelScore.AmountHitedHead * _multiplier;
        }

        private set { }
    }
    public int HitAccuracy
    {
        get
        {
            if (_game.PlayerIsDead)
                return (int)_levelScore.HitAccuracy * _multiplier != 0 ? (int)_levelScore.HitAccuracy * _multiplier / Multiplier : 0;
            else
                return (int)_levelScore.HitAccuracy * _multiplier;
        }

        private set { }
    }
    public int AllReward => KilledZombie + HitedHead + HitAccuracy;
        
    private void Awake()
    {
        _game = GetComponent<Game>();
        _levelScore = GetComponent<LevelScore>();
    }
}
