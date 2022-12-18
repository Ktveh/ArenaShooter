using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumping : MonoBehaviour
{
    [SerializeField] private ControlButton _buttonJumping;
    [SerializeField] private float _force;

    private Rigidbody _rigidbody;
    private CheckingGround _checkingGround;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _checkingGround = GetComponentInChildren<CheckingGround>();
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
        if (_checkingGround.IsTrue)
            _rigidbody.AddRelativeForce(Vector3.up * _force * Time.deltaTime, ForceMode.VelocityChange);
    }
}
