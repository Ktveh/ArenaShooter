using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GettingLeaderboardScore))]
[RequireComponent(typeof(GettingCloudSaving))]
[RequireComponent(typeof(SavingToCloud))]
public class CheckingSaving : MonoBehaviour
{
    [SerializeField] private YandexInitialization _yandexInitialization;
    [SerializeField] private RewritingLocalSave _rewritingLocalSave;

    private GettingLeaderboardScore _gettingLeaderboardScore;
    private GettingCloudSaving _gettingCloudSaving;
    private SavingToCloud _savingToCloud;
    private bool _isGettedScore;
    private bool _isGettedSaving;

    public event UnityAction SaveNotFound;

    private void Awake()
    { 
        _gettingLeaderboardScore = GetComponent<GettingLeaderboardScore>();
        _gettingCloudSaving = GetComponent<GettingCloudSaving>();
        _savingToCloud = GetComponent<SavingToCloud>();
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
        if (_isGettedSaving && _isGettedScore)
        {
            Check();
            enabled = false;
        }
    }

    public void SetScore()
    {
        _isGettedScore = true;
    }

    public void SetSaving()
    {
        _isGettedSaving = true;
    }

    public void RecoverScore()
    {
        SaveNotFound?.Invoke();
        enabled = false;
    }

    private void OnPlayerAuthorizated()
    {
        _gettingLeaderboardScore.enabled = true;
        _gettingCloudSaving.enabled = true;
    }

    private void Check()
    {
        if (_gettingCloudSaving.Try(out CloudSaving[] cloudSavings))
        {
            int scoreInCloud = cloudSavings[0].AmountKilledZombie;
            int scoreInLocal = PlayerPrefs.GetInt(PlayerSaving.AmountKilledZombie);

            if (scoreInCloud < _gettingLeaderboardScore.Current)
            {
                if (scoreInLocal < _gettingLeaderboardScore.Current)
                    SaveNotFound?.Invoke();
                else
                    _savingToCloud.enabled = true;
            }
            else
            {
                if (scoreInLocal > scoreInCloud)
                {
                    _savingToCloud.enabled = true;
                }
                else if (scoreInCloud > scoreInLocal)
                {
                    _rewritingLocalSave.enabled = true;
                }
            }
        }

        enabled = false;
    }
}
