using Agava.YandexGames;
using UnityEngine;

[RequireComponent(typeof(PlayerSaving))]
public class SettingLeaderboardScore : MonoBehaviour
{
    private const string LeaderBoard = "LeaderBoard";

    private PlayerSaving _playerSaving;

    private void OnEnable()
    {
        _playerSaving = GetComponent<PlayerSaving>();

        Leaderboard.GetPlayerEntry(LeaderBoard, (result) =>
        {
            if (result == null || result.score < _playerSaving.CurrentScore)
            {
                Leaderboard.SetScore(LeaderBoard, _playerSaving.CurrentScore);
            }
        });
    }
}
