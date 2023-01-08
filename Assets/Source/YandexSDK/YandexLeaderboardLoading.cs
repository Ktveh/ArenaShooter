using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

public class YandexLeaderboardLoading : MonoBehaviour
{
    private const int MaxAmount = 6;
    private const string Anonymous = "Anonymous";
    private const string LeaderboardName = "TestLeaderboard";

    private List<LeaderboardPlayer> _players;

    public LeaderboardPlayer[] Players => _players.ToArray();

    private void OnEnable()
    {
        _players = new List<LeaderboardPlayer>();

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            for (int i = 0; i < MaxAmount; i++)
            {
                LeaderboardPlayer leaderboardPlayer = new LeaderboardPlayer();

                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = Anonymous;

                leaderboardPlayer.SetValue(result.entries[i].rank, name, result.entries[i].score);
                _players.Add(leaderboardPlayer);
            }
        });
    }
}