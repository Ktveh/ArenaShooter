using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private Joystick _joystick;
    [SerializeField] private ControlButton _runButton;
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _runSpeed;

    private Rigidbody _rigidbody;
    private float _speed;
    private bool _isRunningJoystick;

    private bool _isRunningKeyboard => Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis(Vertical) > 0);

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speed = _defaultSpeed;
    }

    private void FixedUpdate()
    {
        if ((Input.GetAxis(Vertical) != 0) && (Input.GetAxis(Horizontal) != 0) && (_isRunningKeyboard == false))
            _speed = _minSpeed;
        else if(_isRunningKeyboard || _isRunningJoystick)
            _speed = _runSpeed;
        else
            _speed = _defaultSpeed;


        if (Input.GetAxis(Vertical) != 0)
            _rigidbody.AddRelativeForce(Vector3.forward * _speed * Input.GetAxis(Vertical) * Time.deltaTime, ForceMode.Acceleration);

        if ((Input.GetAxis(Horizontal) != 0) && (_isRunningKeyboard == false))
            _rigidbody.AddRelativeForce(Vector3.right * _speed * Input.GetAxis(Horizontal) * Time.deltaTime, ForceMode.Acceleration);
        
        if(_joystick.Vertical != 0)
            _rigidbody.AddRelativeForce(Vector3.forward * _speed * _joystick.Vertical * Time.deltaTime, ForceMode.Acceleration);

        if ((_joystick.Horizontal != 0) && (_isRunningJoystick == false))
            _rigidbody.AddRelativeForce(Vector3.right * _speed * _joystick.Horizontal * Time.deltaTime, ForceMode.Acceleration);
    }

    private void OnEnable()
    {
        _runButton.Down += OnDown;
        _runButton.Up += OnUp;
    }

    private void OnDisable()
    {
        _runButton.Down -= OnDown;
        _runButton.Up -= OnUp;
    }

    private void OnDown()
    {
        _isRunningJoystick = true && (_joystick.Vertical > 0);
    }
    
    private void OnUp()
    {
        _isRunningJoystick = false;
    }
}
