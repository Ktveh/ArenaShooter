using UnityEngine;

[RequireComponent(typeof(LevelScore))]
public class LevelReward : MonoBehaviour
{
    [SerializeField] private int _multiplier;

    private LevelScore _levelScore;

    public int KilledZombie => _levelScore.AmountKilledZombie * _multiplier;
    public int HitedHead => _levelScore.AmountHitedHead * _multiplier;
    public int HitAccuracy => (int)_levelScore.HitAccuracy * _multiplier;
    public int AllReward => KilledZombie + HitedHead + HitAccuracy;

    private void Awake()
    {
        _levelScore = GetComponent<LevelScore>();
    }
}
