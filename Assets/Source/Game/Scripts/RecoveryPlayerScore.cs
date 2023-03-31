using Agava.YandexGames;
using UnityEngine;

[RequireComponent(typeof(PlayerSaving))]
public class RecoveryPlayerScore : MonoBehaviour
{
    private const string LeaderBoard = "LeaderBoard";
    private const string IsFirstLaunch = "IsFirstLaunch";
    private const string False = "False";

    private PlayerSaving _playerSaving;

    private void OnEnable()
    {
        _playerSaving = GetComponent<PlayerSaving>();

        if (PlayerPrefs.GetString(IsFirstLaunch) != False)
        {
            Leaderboard.GetPlayerEntry(LeaderBoard, (result) =>
            {
                if (_playerSaving.CurrentScore != result.score)
                    _playerSaving.Recover(PlayerSaving.AmountKilledZombie, result.score);
            });
        }
    }
}
