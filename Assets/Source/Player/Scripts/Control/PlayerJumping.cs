using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumping : MonoBehaviour
{
    [SerializeField] private ControlButton _buttonJumping;
    [SerializeField] private float _force;

    private Rigidbody _rigidbody;
    private bool _isGround;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnDown();
    }

    private void OnEnable()
    {
        _buttonJumping.Down += OnDown;
    }

    private void OnDisable()
    {
        _buttonJumping.Down -= OnDown;
    }

    private void OnDown()
    {
        if (_isGround)
            _rigidbody.AddRelativeForce( new Vector3(0, _force * Time.deltaTime), ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        _isGround = other != null;
    }

    private void OnTriggerExit(Collider other)
    {
        _isGround = false;
    }
}
