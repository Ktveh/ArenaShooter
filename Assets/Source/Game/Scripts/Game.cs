using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(GameCursorControl))]
[RequireComponent (typeof(GameControllingPlayer))]
public class Game : MonoBehaviour
{
    [SerializeField] private YandexInitialization _yandexInitialization;
    [SerializeField] private InterfaceZombieBar _zombieBar;
    [SerializeField] private Button _buttonPlayingGame;

    private GameCursorControl _gameCursorControl;
    private GameControllingPlayer _gameControllingPlayer;
    private PausingGame _pausingGame;
    private Menu _menu;
    private bool _isMobile;

    public bool IsStarted { get; private set; }

    public event UnityAction<bool> DeviceGeted;
    public event UnityAction LevelCompleted;

    private void Awake()
    {
#if UNITY_EDITOR
        /////////////////---- Editor Only --------/////////////////////////
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Editor Only");
        /////////////////////////////////////////
#endif

        _gameCursorControl = GetComponent<GameCursorControl>();
        _gameControllingPlayer = GetComponent<GameControllingPlayer>();
        _pausingGame = GetComponent<PausingGame>();
        _menu = GetComponentInChildren<Menu>();
    }

    private void Start()
    {
        IsStarted = true;
    }

    private void OnEnable()
    {
        _yandexInitialization.Completed += OnCompleted;
        _zombieBar.AllZombiesDead += OnAllZombiesDead;
        _buttonPlayingGame.onClick.AddListener(Play);
    }

    private void OnDisable()
    {
        _yandexInitialization.Completed -= OnCompleted;
        _zombieBar.AllZombiesDead -= OnAllZombiesDead;
        _buttonPlayingGame.onClick.RemoveListener(Play);
    }

    private void OnCompleted()
    {
        DefineControl();
    }

    private void DefineControl()
    {
        _isMobile = Agava.YandexGames.Device.Type != Agava.YandexGames.DeviceType.Desktop;
        DeviceGeted?.Invoke(_isMobile);

        if (_isMobile)
            _gameCursorControl.Enable();
        else
            _gameCursorControl.Disable();
    }

    private void OnAllZombiesDead()
    {
        _menu.enabled = true;
        _gameCursorControl.Enable();
        _gameControllingPlayer.enabled = true;
        _gameControllingPlayer.DisablePlayerDirection();
        _gameControllingPlayer.DisablePlayerPausingGame();
        LevelCompleted?.Invoke();
    }

    private void Play()
    {
        _gameCursorControl.Disable();
        _gameControllingPlayer.enabled = false;
        _pausingGame.Play();
    }
}
