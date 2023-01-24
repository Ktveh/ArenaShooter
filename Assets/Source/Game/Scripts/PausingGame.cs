using UnityEngine;

[RequireComponent(typeof(GameControllingPlayer))]
public class PausingGame : MonoBehaviour
{
    [SerializeField] private YandexAds _yandexAds;
    [SerializeField] private PlayerPausingGame _playerPausingGame;
    [SerializeField] private PauseMenu _pauseMenu;

    private GameControllingPlayer _gameControllingPlayer;
    private GameCursorControl _gameCursorControl;

    private void Start()
    {
        _gameControllingPlayer = GetComponent<GameControllingPlayer>();
        _gameCursorControl = GetComponent<GameCursorControl>();
    }

    private void OnEnable()
    {
        _yandexAds.Shows += Stop;
        _yandexAds.Showed += Play;
        _yandexAds.Errored += Play;
        _playerPausingGame.Actioned += OnActioned;
    }

    private void OnDisable()
    {
        _yandexAds.Shows -= Stop;
        _yandexAds.Showed -= Play;
        _yandexAds.Errored -= Play;
        _playerPausingGame.Actioned -= OnActioned;
    }

    public void Stop()
    {
        Time.timeScale = 0;
    }

    private void Play()
    {
        Time.timeScale = 1;
    }
    
    private void OnActioned()
    {
        if (_pauseMenu.gameObject.activeSelf == true)
        {
            Play();
            _pauseMenu.gameObject.SetActive(false);
            _gameCursorControl.Disable();
            _gameControllingPlayer.enabled = false;
        }
        else
        {
            Stop();
            _pauseMenu.gameObject.SetActive(true);
            _gameCursorControl.Enable();
            _gameControllingPlayer.enabled = true;
        }

    }
}
