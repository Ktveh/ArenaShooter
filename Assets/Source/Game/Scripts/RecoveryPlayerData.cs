using Agava.YandexGames;
using UnityEngine;

[RequireComponent(typeof(PlayerSaving))]
public class RecoveryPlayerData : MonoBehaviour
{
    private const int Multiplier = 10;
    private const string LeaderBoard = "LeaderBoard";
    private const string IsFirstLaunch = "IsFirstLaunch";
    private const string False = "False";

    [SerializeField] private RewritingLocalSave _rewritedLocalSave;
    [SerializeField] private SavingToCloud _savingToCloud;

    private PlayerSaving _playerSaving;
    private int _score;
    private int _money;

    private void Awake()
    {
        if(PlayerPrefs.GetString(IsFirstLaunch) == False)
            enabled = false;

        _playerSaving = GetComponent<PlayerSaving>();
    }

    private void OnEnable()
    {
        _rewritedLocalSave.Rewrited += OnRewrited;
    }

    private void OnDisable()
    {
        _rewritedLocalSave.Rewrited -= OnRewrited;
    }

    private void OnRewrited()
    {
        Leaderboard.GetPlayerEntry(LeaderBoard, (result) => _score = result.score);
        _playerSaving.Recover(PlayerSaving.AmountKilledZombie, _score);
        _money = _score * Multiplier;
        _playerSaving.Recover(PlayerSaving.Money, _money);
        _savingToCloud.enabled = true;
        PlayerPrefs.SetString(IsFirstLaunch, False);
    }
}
