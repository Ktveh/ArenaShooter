using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GettingCloudSaving))]
[RequireComponent(typeof(SavingToCloud))]
public class CheckingSaving : MonoBehaviour
{
    [SerializeField] private YandexInitialization _yandexInitialization;
    [SerializeField] private RewritingLocalSave _rewritingLocalSave;

    private GettingCloudSaving _gettingCloudSaving;
    private SavingToCloud _savingToCloud;
    private bool _localScoreMore;

    public event UnityAction SaveNotFound;

    private void Awake()
    { 
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
        if (_gettingCloudSaving.IsSuccess)
        {
            if (_localScoreMore == false)
            {
                _rewritingLocalSave.enabled = true;
                enabled = false;
            }
        }

        if (_gettingCloudSaving.IsError)
        {
            SaveNotFound?.Invoke();
            enabled = false;
        }
    }

    private void OnPlayerAuthorizated()
    {
        _gettingCloudSaving.enabled = true;

        if (_gettingCloudSaving.Try(out CloudSaving[] cloudSavings))
        {
            Debug.Log("Geted cloud saving");

            int scoreInCloud = cloudSavings[0].AmountKilledZombie;
            int scoreInLocal = PlayerPrefs.GetInt(PlayerSaving.AmountKilledZombie);
            _localScoreMore = scoreInLocal > scoreInCloud;

            if (_localScoreMore)
            {
                _savingToCloud.enabled = true;
                enabled = false;
            }
        }
    }
}
