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

    private ZombieTargeter _zombieTargeter;
    private SoundTarget _soundTarget;
    private MainTarget _mainTarget;
    private RandomTarget _randomTarget;
    private Target _currentTarget;
    private int _currentRaycast;
    private bool _isAttentive;

    public bool IsAttentive => _isAttentive;
    public Target CurrentTarget => _currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SoundTarget>())
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
        FoundTargets(_angleDetected, _forwardDistanceDetected);
        //FoundTargets(360, _aroundDistanceDetected);
        SetCurrentTarget();
    }

    public void RemoveCurrentTarget()
    {
        if (!_isAttentive)
        {
            _randomTarget = null;
            SetCurrentTarget();
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
            _angleDetected = 360;
        }

        _currentRaycast++;
        if (_currentRaycast > _amountRaycasts)
        {
            _currentRaycast = 0;
        }

        float angle = _currentRaycast * angleDetected * Mathf.Deg2Rad / _amountRaycasts;

        float x = Mathf.Sin(angle);
        float z = Mathf.Cos(angle);

        GetRaycast(transform.TransformDirection(new Vector3(x, 0, z)), distance);

        if (x != 0)
        {
            GetRaycast(transform.TransformDirection(new Vector3(-x, 0, z)), distance);
        }
    }  

    private void GetRaycast(Vector3 direction, float distance)
    {
        Vector3 startPosition = transform.position + _offset;
        RaycastHit hit;

        if (Physics.Raycast(startPosition, direction, out hit, distance))
        {   
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
        _randomTarget = Instantiate(_template, transform.position, transform.rotation);
        _randomTarget.SetPosition(position, spread);
    }
}
