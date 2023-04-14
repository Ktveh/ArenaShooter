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

#if YANDEX_GAMES
        Leaderboard.GetPlayerEntry(LeaderBoard, (result) =>
        {
            if (result == null || result.score < _playerSaving.CurrentScore)
            {
                Leaderboard.SetScore(LeaderBoard, _playerSaving.CurrentScore);
            }
        });
#endif

#if VK_GAMES
        DungeonGames.VKGames.Leaderboard.ShowLeaderboard(_playerSaving.CurrentScore);
#endif
    }
}
