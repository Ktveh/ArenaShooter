using Agava.YandexGames;
using UnityEngine;

[RequireComponent(typeof(PlayerSaving))]
public class RecoveryPlayerScore : MonoBehaviour
{
    private const string LeaderBoard = "LeaderBoard";

    [SerializeField] private YandexInitialization _yandexInitialization;

    private PlayerSaving _playerSaving;

    private void OnEnable()
    {
        _playerSaving = GetComponent<PlayerSaving>();

        _yandexInitialization.Completed += OnCompleted;
    }

    private void OnDisable()
    {
        _yandexInitialization.Completed -= OnCompleted;
    }

    private void OnCompleted()
    {
        Leaderboard.GetPlayerEntry(LeaderBoard, (result) =>
        {
            if (_playerSaving.CurrentScore != result.score)
                _playerSaving.Recover(result.score);
        });
    }
}
