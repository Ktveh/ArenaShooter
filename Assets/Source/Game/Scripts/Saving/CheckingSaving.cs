using Agava.YandexGames;
using UnityEngine;

[RequireComponent(typeof(GettingCloudSaving))]
public class CheckingSaving : MonoBehaviour
{
    private const string LeaderBoard = "LeaderBoard";

    [SerializeField] private YandexInitialization _yandexInitialization;
    [SerializeField] private PlayerSaving _playerSaving;
    [SerializeField] private RestoringLocalSave _restoringLocalSave;

    private GettingCloudSaving _gettingCloudSaving;

    private int _score;

    private void Awake()
    {
        _gettingCloudSaving = GetComponent<GettingCloudSaving>();
    }

    private void OnEnable()
    {
        _yandexInitialization.PlayerAuthorizated += OnPlayerAuthorizated;
    }

    private void OnDisable()
    {
        _yandexInitialization.PlayerAuthorizated -= OnPlayerAuthorizated;
    }

    private void Update()
    {
        if (_restoringLocalSave.enabled)
        {
            enabled = false;
            return;
        }

        if (_gettingCloudSaving.Try(out CloudSaving[] cloudSavings))
            _restoringLocalSave.enabled = true;
    }

    private void OnPlayerAuthorizated()
    {
        Leaderboard.GetPlayerEntry(LeaderBoard, (result) => _score = result.score);

        if (_score != _playerSaving.CurrentScore)
            _gettingCloudSaving.enabled = true;
    }
}
