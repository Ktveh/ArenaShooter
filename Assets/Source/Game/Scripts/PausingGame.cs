using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Game))]
[RequireComponent(typeof(GameControllingPlayer))]
[RequireComponent(typeof(GameCursorControl))]
public class PausingGame : MonoBehaviour
{
    [SerializeField] private YandexAds _yandexAds;
    [SerializeField] private PlayerPausingGame _playerPausingGame;
    [SerializeField] private PauseMenu _pauseMenu;

    private Game _game;
    private GameControllingPlayer _gameControllingPlayer;
    private GameCursorControl _gameCursorControl;

    public event UnityAction Played;
    public event UnityAction Paused;

    private void Start()
    {
        _game = GetComponent<Game>();
        _gameControllingPlayer = GetComponent<GameControllingPlayer>();
        _gameCursorControl = GetComponent<GameCursorControl>();
    }

    private void Update()
    {
        if(_pauseMenu.gameObject.activeSelf)
            _gameCursorControl.Enable();
    }

    private void OnEnable()
    {
        _yandexAds.Shows += Stop;
        _yandexAds.Showed += Play;
        _yandexAds.Errored += Play;
        _playerPausingGame.Paused += OnPaused;
    }

    private void OnDisable()
    {
        _yandexAds.Shows -= Stop;
        _yandexAds.Showed -= Play;
        _yandexAds.Errored -= Play;
        _playerPausingGame.Paused -= OnPaused;
    }

    public void Stop()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        Time.timeScale = 1;
        Played?.Invoke();
    }
    
    private void OnPaused()
    {
        if (_game.IsLevelCompleted)
            return;

        if (_pauseMenu.gameObject.activeSelf)
        {
            Play();
            _pauseMenu.gameObject.SetActive(false);
            _gameControllingPlayer.enabled = true;
            Played?.Invoke();

            if (_game.IsMobile)
                _gameCursorControl.Enable();
            else
                _gameCursorControl.Disable();
        }
        else
        {
            Stop();
            _pauseMenu.gameObject.SetActive(true);
            _gameCursorControl.Enable();
            _gameControllingPlayer.enabled = false;
            Paused?.Invoke();
        }

    }
}
