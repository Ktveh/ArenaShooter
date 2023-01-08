using Agava.YandexGames;
using UnityEngine;

public class YandexSavingPlayerScore : MonoBehaviour
{
    private const string LeaderboardName = "TestLeaderboard";

    [SerializeField] private PlayerZombieKillCounter _playerZombieKillCounter;

    private void OnEnable()
    {
        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null)
            {
                Leaderboard.SetScore(LeaderboardName, _playerZombieKillCounter.Count);
            }
            else
            {
                if (result.score < _playerZombieKillCounter.Count)
                    Leaderboard.SetScore(LeaderboardName, _playerZombieKillCounter.Count);
            }
        });
    }
}