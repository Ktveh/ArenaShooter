using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GameCursorControl))]
[RequireComponent (typeof(GameDisablingPlayerControl))]
public class Game : MonoBehaviour
{
    [SerializeField] private YandexInitialization _yandexInitialization;
    [SerializeField] private InterfaceZombieBar _zombieBar;

    private GameCursorControl _gameCursorControl;
    private GameDisablingPlayerControl _gameDisablingPlayerControl;
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
        _gameDisablingPlayerControl = GetComponent<GameDisablingPlayerControl>();
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
    }

    private void OnDisable()
    {
        _yandexInitialization.Completed -= OnCompleted;
        _zombieBar.AllZombiesDead -= OnAllZombiesDead;
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
        _gameDisablingPlayerControl.enabled = true;
        LevelCompleted?.Invoke();
    }
}
