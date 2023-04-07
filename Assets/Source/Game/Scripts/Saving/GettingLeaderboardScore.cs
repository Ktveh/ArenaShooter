using Agava.YandexGames;
using UnityEngine;

[RequireComponent(typeof(CheckingSaving))]
public class GettingLeaderboardScore : MonoBehaviour
{
    private const string LeaderBoard = "LeaderBoard";

    private CheckingSaving _checkingSaving;

    public int Current { get; private set; }

    private void Awake()
    {
        _checkingSaving = GetComponent<CheckingSaving>();
    }

    private void OnEnable()
    {
        Leaderboard.GetPlayerEntry(LeaderBoard, OnSuccess);
    }

    private void OnSuccess(LeaderboardEntryResponse result)
    {
        Current = result.score;
        _checkingSaving.SetScore();
    }
}
