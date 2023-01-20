using UnityEngine;

public class Interface : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Menu _menu;

    private TouchControl _touchControl;

    private void Awake()
    {
        _touchControl = GetComponentInChildren<TouchControl>();
    }

    private void OnEnable()
    {
        _game.DeviceGeted += OnDeviceGeted;
        _menu.Showed += OnShowed;
    }

    private void OnDisable()
    {
        _game.DeviceGeted -= OnDeviceGeted;
        _menu.Showed -= OnShowed;
    }

    private void OnDeviceGeted(bool isMobile)
    {
        if(isMobile)
            _touchControl.gameObject.SetActive(true);
    }

    private void OnShowed()
    {
        gameObject.SetActive(false);
    }
}
