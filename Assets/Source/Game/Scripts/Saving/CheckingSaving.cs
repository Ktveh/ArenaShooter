using Agava.YandexGames;
using UnityEngine;

[RequireComponent(typeof(GettingCloudSaving))]
public class CheckingSaving : MonoBehaviour
{
    [SerializeField] private YandexInitialization _yandexInitialization;
    [SerializeField] private PlayerSaving _playerSaving;
    [SerializeField] private RewritingLocalSave _rewritingLocalSave;

    private GettingCloudSaving _gettingCloudSaving;

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
        if (_rewritingLocalSave.enabled)
        {
            enabled = false;
            return;
        }

        if (_gettingCloudSaving.Try(out CloudSaving[] cloudSavings))
            _rewritingLocalSave.enabled = true;
    }

    private void OnPlayerAuthorizated()
    {
        _gettingCloudSaving.enabled = true;
    }
}
