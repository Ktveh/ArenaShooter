using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(GameCursorControl))]
[RequireComponent (typeof(GameControllingPlayer))]
[RequireComponent (typeof(PausingGame))]
[RequireComponent (typeof(ControllingAudio))]
public class Game : MonoBehaviour
{
    [SerializeField] private YandexInitialization _yandexInitialization;
    [SerializeField] private InterfaceZombieBar _zombieBar;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Button _buttonPlayingGame;

    private GameCursorControl _gameCursorControl;
    private GameControllingPlayer _gameControllingPlayer;
    private PausingGame _pausingGame;
    private ControllingAudio _controllingAudio;
    private SettingLeaderboardScore _settingLeaderboardScore;
    private Menu _menu;
    private bool _isMobile;

    public bool IsStarted { get; private set; }
    public bool PlayerIsDead { get; private set; }
    public bool IsMobile => _isMobile;

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
        _controllingAudio = GetComponent<ControllingAudio>();
        _settingLeaderboardScore = GetComponent<SettingLeaderboardScore>();
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
        _playerHealth.Deaded += OnDeaded;
    }

    private void OnDisable()
    {
        _yandexInitialization.Completed -= OnCompleted;
        _zombieBar.AllZombiesDead -= OnAllZombiesDead;
        _buttonPlayingGame.onClick.RemoveListener(Play);
        _playerHealth.Deaded -= OnDeaded;
    }

    private void OnCompleted()
    {
        DefineControl();
    }

    private void DefineControl()
    {
        _isMobile = Agava.YandexGames.Device.Type != Agava.YandexGames.DeviceType.Desktop ? true : false;
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
        _gameControllingPlayer.enabled = false;
        _gameControllingPlayer.EnableWeaponSelecting();
        _controllingAudio.enabled = false;
        _settingLeaderboardScore.enabled = true;
        LevelCompleted?.Invoke();
    }

    private void Play()
    {
        _gameCursorControl.Disable();
        _pausingGame.Play();
    }

    private void OnDeaded()
    {
        PlayerIsDead = true;
        OnAllZombiesDead();
    }
}
