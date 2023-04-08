using UnityEngine;

[RequireComponent(typeof(PlayerSaving))]
public class RecoveryPlayerData : MonoBehaviour
{
    private const int Multiplier = 10;

    [SerializeField] private SavingToCloud _savingToCloud;
    [SerializeField] private CheckingSaving _checkingSaving;
    [SerializeField] private GettingLeaderboardScore _gettingLeaderboardScore;
    [SerializeField] private RewritingLocalSave _writingLocalSave;

    private PlayerSaving _playerSaving;
    private int _score;
    private int _money;

    private void Awake()
    {
        _playerSaving = GetComponent<PlayerSaving>();
    }

    private void OnEnable()
    {
        _checkingSaving.SaveNotFound += OnSaveNotFound;
        _writingLocalSave.Rewrited += OnRewrited;
    }

    private void OnDisable()
    {
        _checkingSaving.SaveNotFound -= OnSaveNotFound;
        _writingLocalSave.Rewrited -= OnRewrited;
    }

    private void OnSaveNotFound()
    {
        _score = _gettingLeaderboardScore.Current;
        _playerSaving.Recover(PlayerSaving.AmountKilledZombie, _score);
        _money = _score * Multiplier;
        _playerSaving.Recover(PlayerSaving.Money, _money);
        _savingToCloud.enabled = true;
        enabled = false;
    }

    private void OnRewrited()
    {
        _score = _gettingLeaderboardScore.Current;
        _playerSaving.Recover(PlayerSaving.AmountKilledZombie, _score);
        _savingToCloud.enabled = true;
        enabled = false;
    }
}
