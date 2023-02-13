using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTargeter : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _angleDetected;
    [SerializeField] private float _forwardDistanceDetected;
    [SerializeField] private float _aroundDistanceDetected;
    [SerializeField] private int _amountRaycasts;
    [SerializeField] private RandomTarget _template;
    [SerializeField] private float _durationCurrenTarget;

    private ZombieTargeter _zombieTargeter;
    private SoundTarget _soundTarget;
    private MainTarget _mainTarget;
    private RandomTarget _randomTarget;
    private Target _currentTarget;
    private int _currentRaycast;
    private bool _isAttentive = false;
    private float _ellapsedTime = 0;

    public bool IsAttentive => _isAttentive;
    public Target CurrentTarget => _currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SoundTarget>() && !_isAttentive)
        {
            _soundTarget = other.GetComponent<SoundTarget>();
            RandomTarget(_soundTarget.transform.position, false);
            _isAttentive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SoundTarget>())
        {
            _soundTarget = null;
        }
    }

    private void Start()
    {
        RandomTarget(transform.position, true);
    }

    private void Update()
    {
        _ellapsedTime += Time.deltaTime;
        FoundTargets(_angleDetected, _forwardDistanceDetected);
        SetCurrentTarget();
        if (_ellapsedTime > _durationCurrenTarget)
        {
            _ellapsedTime = 0;
            _mainTarget = null;
            _soundTarget = null;
            _zombieTargeter = null;
            _isAttentive = false;
            RemoveCurrentTarget();
        }
    }

    public void RemoveCurrentTarget()
    {
        if (!_isAttentive)
        {
            RandomTarget(transform.position, true);
        }
    }

    private void SetCurrentTarget()
    {
        if (_randomTarget != null)
        {
            _currentTarget = _randomTarget;
        }
        else
        {
            _isAttentive = false;
            RandomTarget(transform.position, true);
        }
    }

    private void FoundTargets(float angleDetected, float distance)
    {
        if (_isAttentive)
        {
            angleDetected = 360;
        }

        _currentRaycast++;
        if (_currentRaycast > _amountRaycasts)
        {
            _currentRaycast = -_amountRaycasts;
        }

        float angle = _currentRaycast * angleDetected * Mathf.Deg2Rad / _amountRaycasts;

        float x = Mathf.Sin(angle);
        float z = Mathf.Cos(angle);

        GetRaycast(transform.TransformDirection(new Vector3(x, 0, z)), distance);
    }  

    private void GetRaycast(Vector3 direction, float distance)
    {
        Vector3 startPosition = transform.position + _offset;
        RaycastHit hit;

        if (Physics.Raycast(startPosition, direction, out hit, distance))
        {
            Debug.DrawRay(startPosition, direction, Color.red);
            if (hit.collider.gameObject.GetComponent<MainTarget>())
            {
                _mainTarget = hit.collider.gameObject.GetComponent<MainTarget>();
                RandomTarget(_mainTarget.transform.position, false);
                _isAttentive = true;
            }
            else if (hit.collider.gameObject.GetComponent<ZombieTargeter>() && !_isAttentive)
            {
                _zombieTargeter = hit.collider.gameObject.GetComponent<ZombieTargeter>();
                if (_zombieTargeter.IsAttentive)
                {
                    RandomTarget(_zombieTargeter.CurrentTarget.transform.position, false);
                    _isAttentive = true;
                }
                else
                {
                    _zombieTargeter = null;
                    _isAttentive = false;
                }
            }
        }
    }

    private void RandomTarget(Vector3 position, bool spread)
    {
        _ellapsedTime = 0;
        if (_randomTarget == null)
            _randomTarget = Instantiate(_template, transform.position, transform.rotation);
        _randomTarget.SetPosition(position, spread);
    }
}
