using UnityEngine;

public class CameraDirection : MonoBehaviour
{
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _orintation;
    [SerializeField] private float _sensitivity;

    private float _directionX;
    private float _directionY;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        transform.eulerAngles = new Vector3(0, 0);
    }

    private void LateUpdate()
    {
        if ((Input.GetAxisRaw(MouseX) != 0) || (Input.GetAxisRaw(MouseY) != 0))
        {
            float mouseX = Input.GetAxisRaw(MouseX) * _sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw(MouseY) * _sensitivity * Time.deltaTime;
            _directionX -= mouseY;
            _directionY += mouseX;
        }
        else if ((_joystick.Horizontal != 0) || (_joystick.Vertical != 0))
        {
            float mouseX = _joystick.Horizontal * _sensitivity * Time.deltaTime;
            float mouseY = _joystick.Vertical * _sensitivity * Time.deltaTime;
            _directionX -= mouseY;
            _directionY += mouseX;
        }

        _directionX = Mathf.Clamp(_directionX, -90, 90);
        transform.rotation = Quaternion.Euler(_directionX, _directionY, 0);
        _orintation.rotation = Quaternion.Euler(0, _directionY, 0);
    }
}
