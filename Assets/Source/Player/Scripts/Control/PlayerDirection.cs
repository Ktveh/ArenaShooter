using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    [SerializeField] private Game _game;
    [SerializeField] private SettingCameraSensitivity _settingCameraSensitivity;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _orintation;
    [SerializeField] private float _sensitivity;

    private float _directionX;
    private float _directionY;
    private bool _isMobile;

    public float Sensitivity => _sensitivity;

    private void LateUpdate()
    {
        if (_isMobile)
            CameraRotation(new Vector2(_joystick.Horizontal, _joystick.Vertical));
        else
            CameraRotation(new Vector2(Input.GetAxisRaw(MouseX), Input.GetAxisRaw(MouseY)));
    }

    private void OnEnable()
    {
        _settingCameraSensitivity.Changed += OnChanged;
        _game.DeviceGeted += OnDeviceGeted;
    }

    private void OnDisable()
    {
        _settingCameraSensitivity.Changed -= OnChanged;
        _game.DeviceGeted -= OnDeviceGeted;
    }

    private void CameraRotation(Vector2 direction)
    {
        float mouseX = direction.x * _sensitivity * Time.deltaTime;
        float mouseY = direction.y * _sensitivity * Time.deltaTime;
        _directionX -= mouseY;
        _directionY += mouseX;
        _directionX = Mathf.Clamp(_directionX, -90, 90);
        transform.rotation = Quaternion.Euler(_directionX, _directionY, 0);
        _orintation.rotation = Quaternion.Euler(0, _directionY, 0);
    }

    private void OnChanged(float value)
    {
        _sensitivity = value;
    }

    private void OnDeviceGeted(bool isMobile)
    {
        _isMobile = isMobile;
    }
}
