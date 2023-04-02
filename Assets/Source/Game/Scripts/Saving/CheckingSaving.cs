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

    public void SaveScore()
    {
        if (_gettingCloudSaving.Try(out CloudSaving[] cloudSavings))
        {
            int scoreInCloud = cloudSavings[0].AmountKilledZombie;
            int scoreInLocal = PlayerPrefs.GetInt(PlayerSaving.AmountKilledZombie);

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

    public void RecoverScore()
    {
        SaveNotFound?.Invoke();
    }

    private void OnPlayerAuthorizated()
    {
        _gettingCloudSaving.enabled = true;
    }
}
