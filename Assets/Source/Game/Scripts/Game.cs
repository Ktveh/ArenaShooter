using UnityEngine;

[RequireComponent(typeof(GameEnablingCursor))]
public class Game : MonoBehaviour
{
    [SerializeField] private YandexInitialization _yandexInitialization;

    private GameEnablingCursor _gameEnablingCursor;

    private void Awake()
    {
        _gameEnablingCursor = GetComponent<GameEnablingCursor>();
    }

    private void OnEnable()
    {
        _yandexInitialization.Completed += OnCompleted;
    }

    private void OnDisable()
    {
        _yandexInitialization.Completed -= OnCompleted;
    }

    private void OnCompleted()
    {
        DefineControl();
    }

    private void DefineControl()
    {
        if (Agava.YandexGames.Device.Type != Agava.YandexGames.DeviceType.Desktop)
            _gameEnablingCursor.enabled = true;
    }
}
