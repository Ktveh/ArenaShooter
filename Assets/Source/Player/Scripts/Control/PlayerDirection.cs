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
    [SerializeField] private float _durationRecoil;

    private PlayerWeaponSelecting _playerWeaponSelecting;
    private float _weaponRecoil;
    private float _directionX;
    private float _directionY;
    private float _time;
    private bool _isMobile;
    private bool _isShot;

    public float Sensitivity => _sensitivity;

    private void Awake()
    {
        _playerWeaponSelecting = GetComponent<PlayerWeaponSelecting>();
    }

    private void LateUpdate()
    {
        if (_isShot)
        {
            _time += Time.deltaTime;

            if (_time > _durationRecoil)
            {
                _weaponRecoil = 0;
                _time = 0;
                _isShot = false;
            }
        }

        if (_isMobile)
            CameraRotation(new Vector2(_joystick.Horizontal, _joystick.Vertical + _weaponRecoil));
        else
            CameraRotation(new Vector2(Input.GetAxisRaw(MouseX), Input.GetAxisRaw(MouseY) + _weaponRecoil));
    }

    private void OnEnable()
    {
        _settingCameraSensitivity.Changed += OnChanged;
        _game.DeviceGeted += OnDeviceGeted;
        _playerWeaponSelecting.Selected += OnSelected;
    }

    private void OnDisable()
    {
        _settingCameraSensitivity.Changed -= OnChanged;
        _game.DeviceGeted -= OnDeviceGeted;
        _playerWeaponSelecting.Selected -= OnSelected;
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
        if(value != 0)
            _sensitivity = value;
    }

    private void OnDeviceGeted(bool isMobile)
    {
        _isMobile = isMobile;
    }

    private void OnSelected()
    {
        if (_playerWeaponSelecting.LastWeapon != null)
        {
            if (_playerWeaponSelecting.LastWeapon.TryGetComponent(out WeaponShooting lastWeaponShooting))
                lastWeaponShooting.Shooted -= OnShooted;
        }

        if (_playerWeaponSelecting.CurrentWeapon.TryGetComponent(out WeaponShooting currentWeaponShooting))
        {
            currentWeaponShooting.Shooted += OnShooted;
        }
    }

    private void OnShooted()
    {
        _weaponRecoil = _playerWeaponSelecting.CurrentWeapon.ForceRecoil;
        _isShot = true;
    }
}
