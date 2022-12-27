using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _orintation;
    [SerializeField] private float _sensitivity;

    private float _directionX;
    private float _directionY;
    private float _forceRecoil;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        CameraRotation(new Vector2(Input.GetAxisRaw(MouseX), Input.GetAxisRaw(MouseY)));
        CameraRotation(new Vector2(_joystick.Horizontal, _joystick.Vertical));
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
}
