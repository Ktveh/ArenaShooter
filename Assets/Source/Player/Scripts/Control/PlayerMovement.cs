using UnityEngine;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(PlayerJumping))]
public class PlayerMovement : MonoBehaviour
{
    private const float SpeedChangeRate = 10f;
    private const float Multiplier = 1000f;
    private const float SpeedOffset = 0.1f;
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    [SerializeField] private Joystick _joystick;
    [SerializeField] private ControlButton _runButton;
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _runSpeed;

    private CharacterController _characterController;
    private PlayerJumping _playerJumping;
    private float _speed;
    private bool _isRunningJoystick;

    private bool _isRunningKeyboard => Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis(Vertical) > 0);

    public bool IsWalking => _speed > 0; 
    public bool IsRunning => _isRunningJoystick || _isRunningKeyboard;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerJumping = GetComponent<PlayerJumping>();
    }

    private void Update()
    {
        Move(new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical)));
        Move(new Vector2(_joystick.Horizontal, _joystick.Vertical));
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

        _speed = 0;
    }

    private void OnDown()
    {
        _isRunningJoystick = true && (_joystick.Vertical > 0);
    }
    
    private void OnUp()
    {
        _isRunningJoystick = false;
    }

    private void Move(Vector2 direction)
    {
        float targetSpeed = _isRunningKeyboard || _isRunningJoystick ? _runSpeed : _defaultSpeed;

        if (direction == Vector2.zero)
            targetSpeed = 0f;

        float currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z).magnitude;

        if ((currentHorizontalSpeed < targetSpeed - SpeedOffset) || (currentHorizontalSpeed > targetSpeed + SpeedOffset))
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * direction.magnitude, Time.deltaTime * SpeedChangeRate);
            _speed = Mathf.Round(_speed * Multiplier) / Multiplier;
        }
        else
        {
            _speed = targetSpeed;
        }

        Vector3 inputDirection = new Vector3(direction.x, 0, direction.y).normalized;

        if (direction != Vector2.zero)
            inputDirection = transform.right * direction.x + transform.forward * direction.y;

        _characterController.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0, _playerJumping.VerticalVelocity, 0) * Time.deltaTime);
    }
}
