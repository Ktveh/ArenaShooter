using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    //private const float GroundedRadius = 0.5f;
    //private const float Gravity = -15.0f;
    //private const float GroundedOffset = 1f;
    //private const float TerminalVelocity = 53.0f;
    //private const float JumpTimeout = 0.1f;
    //private const float FallTimeout = 0.15f;
    //private const float CorrectionVerticalPosition = -2f;

    //[SerializeField] private ControlButton _buttonJumping;
    //[SerializeField] private LayerMask _groundLayers;
    //[SerializeField] private float _jumpHeight = 1.2f;

    //private float _jumpTimeoutDelta;
    //private float _fallTimeoutDelta;
    //private float _verticalVelocity;
    //private bool _isGrounded = true;
    //private bool _isPressed;

    //public float VerticalVelocity => _verticalVelocity;

    //private void Start()
    //{
    //    _jumpTimeoutDelta = JumpTimeout;
    //    _fallTimeoutDelta = FallTimeout;
    //}

    //private void Update()
    //{
    //    CheckGrounded();
    //    Jump();
    //    Gravitate();
    //}

    //private void OnEnable()
    //{
    //    _buttonJumping.Down += OnDown;
    //}

    //private void OnDisable()
    //{
    //    _buttonJumping.Down -= OnDown;

    //    _isGrounded = false;
    //}

    //private void OnDown()
    //{
    //    _isPressed = true;
    //}

    //private void CheckGrounded()
    //{
    //    Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
    //    _isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);
    //}

    //private void Jump()
    //{
    //    if (_isGrounded)
    //    {
    //        _fallTimeoutDelta = FallTimeout;

    //        if (_verticalVelocity < 0f)
    //            _verticalVelocity = CorrectionVerticalPosition;

    //        if (Input.GetKeyDown(KeyCode.Space) || _isPressed && (_jumpTimeoutDelta <= 0))
    //            _verticalVelocity = Mathf.Sqrt(_jumpHeight * CorrectionVerticalPosition * Gravity);

    //        if (_jumpTimeoutDelta >= 0)
    //            _jumpTimeoutDelta -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        _jumpTimeoutDelta = JumpTimeout;

    //        if (_fallTimeoutDelta >= 0)
    //            _fallTimeoutDelta -= Time.deltaTime;
    //    }

    //    _isPressed = false;
    //}

    //private void Gravitate()
    //{
    //    if (_verticalVelocity < TerminalVelocity)
    //        _verticalVelocity += Gravity * Time.deltaTime;
    //}
}
