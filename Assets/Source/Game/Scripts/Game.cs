using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(GameCursorControl))]
[RequireComponent (typeof(GameControllingPlayer))]
[RequireComponent (typeof(PausingGame))]
[RequireComponent (typeof(ControllingAudio))]
[RequireComponent (typeof(SettingLeaderboardScore))]
public class Game : MonoBehaviour
{
    [SerializeField] private YandexInitialization _yandexInitialization;
    [SerializeField] private YandexAds _yandexAds;
    [SerializeField] private InterfaceZombieBar _zombieBar;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Button _buttonPlayingGame;

    private GameCursorControl _gameCursorControl;
    private GameControllingPlayer _gameControllingPlayer;
    private PausingGame _pausingGame;
    private ControllingAudio _controllingAudio;
    private SettingLeaderboardScore _settingLeaderboardScore;
    private Menu _menu;

    public bool IsStarted { get; private set; }
    public bool PlayerIsDead { get; private set; }
    public bool IsLevelCompleted { get; private set; }
    public bool IsMobile { get; private set; }

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

    private void Update()
    {
        if (IsLevelCompleted)
            _gameCursorControl.Enable();
    }

    private void OnEnable()
    {
        _yandexInitialization.Completed += OnCompleted;
        _zombieBar.AllZombiesDead += OnAllZombiesDead;
        _buttonPlayingGame.onClick.AddListener(Play);
        _playerHealth.Deaded += OnDeaded;
        _yandexAds.Showed += OnShwoed;
        _yandexAds.Errored += OnShwoed;
    }

    private void OnDisable()
    {
        _yandexInitialization.Completed -= OnCompleted;
        _zombieBar.AllZombiesDead -= OnAllZombiesDead;
        _buttonPlayingGame.onClick.RemoveListener(Play);
        _playerHealth.Deaded -= OnDeaded;
        _yandexAds.Showed -= OnShwoed;
        _yandexAds.Errored -= OnShwoed;
    }

    private void OnCompleted()
    {
        DefineControl();
    }

    private void DefineControl()
    {
        IsMobile = Agava.YandexGames.Device.Type != Agava.YandexGames.DeviceType.Desktop ? true : false;
        DeviceGeted?.Invoke(IsMobile);

        if (IsMobile)
            _gameCursorControl.Enable();
        else
            _gameCursorControl.Disable();
    }

    private void OnAllZombiesDead()
    {
        IsLevelCompleted = true;
        _controllingAudio.enabled = false;
        _gameControllingPlayer.enabled = false;
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

    private void OnShwoed()
    {
        _controllingAudio.enabled = false;
        _menu.enabled = true;
        _gameControllingPlayer.EnableWeaponSelecting();
        _settingLeaderboardScore.enabled = true;
    }
}
