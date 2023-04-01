using Agava.YandexGames;
using UnityEngine;

[RequireComponent(typeof(PlayerSaving))]
public class RecoveryPlayerData : MonoBehaviour
{
    private const int Multiplier = 10;
    private const string LeaderBoard = "LeaderBoard";

    [SerializeField] private SavingToCloud _savingToCloud;
    [SerializeField] private CheckingSaving _checkingSaving;

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
    }

    private void OnDisable()
    {
        _checkingSaving.SaveNotFound -= OnSaveNotFound;
    }

    private void OnSaveNotFound()
    {
        Leaderboard.GetPlayerEntry(LeaderBoard, OnSuccess);
    }

    private void OnSuccess(LeaderboardEntryResponse result)
    {
        _score = result.score;
        _playerSaving.Recover(PlayerSaving.AmountKilledZombie, _score);
        _money = _score * Multiplier;
        _playerSaving.Recover(PlayerSaving.Money, _money);
        _savingToCloud.enabled = true;
        enabled = false;
    }
}
