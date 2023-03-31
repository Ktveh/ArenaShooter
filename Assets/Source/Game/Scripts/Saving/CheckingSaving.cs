using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GettingCloudSaving))]
public class CheckingSaving : MonoBehaviour
{
    [SerializeField] private YandexInitialization _yandexInitialization;
    [SerializeField] private RewritingLocalSave _rewritingLocalSave;

    private GettingCloudSaving _gettingCloudSaving;

    public event UnityAction SaveNotFound;

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

        if (_gettingCloudSaving.IsSuccess)
            _rewritingLocalSave.enabled = true;

        if (_gettingCloudSaving.IsError)
            SaveNotFound?.Invoke();
    }

    private void OnPlayerAuthorizated()
    {
        _gettingCloudSaving.enabled = true;
    }
}
