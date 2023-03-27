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
    private int _levelAttective = 0;
    private float _ellapsedTime = 0;

    public int LevelAttentive => _levelAttective;
    public Target CurrentTarget => _currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SoundTarget>() && _levelAttective < 2)
        {
            _soundTarget = other.GetComponent<SoundTarget>();
            RandomTarget(_soundTarget.transform.position, false);
            _levelAttective = 2;
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
            if (_levelAttective > 0)
                _levelAttective--;
            _durationCurrenTarget = Random.Range(3, 7);
            RemoveCurrentTarget();
        }
    }

    public void RemoveCurrentTarget(bool setAttective = false)
    {
        if (_levelAttective < 3)
        {
            if (setAttective)
            {
                _levelAttective++;
            }
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
            _levelAttective = 0;
            RandomTarget(transform.position, true);
        }
    }

    private void FoundTargets(float angleDetected, float distance)
    {
        if (_levelAttective >=2)
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

        if (x != 0)
        {
            GetRaycast(transform.TransformDirection(new Vector3(-x, 0, z)), distance);
        }
    }  

    private void GetRaycast(Vector3 direction, float distance)
    {
        Vector3 startPosition = transform.position + _offset;
        RaycastHit hit;

        Debug.DrawRay(startPosition, direction * distance, Color.red);
        if (Physics.Raycast(startPosition, direction, out hit, distance))
        {
            if (hit.collider.gameObject.GetComponent<MainTarget>())
            {
                _mainTarget = hit.collider.gameObject.GetComponent<MainTarget>();
                RandomTarget(_mainTarget.transform.position, false);
                _levelAttective = 3;
            }
            else if (hit.collider.gameObject.GetComponent<ZombieTargeter>() && _levelAttective <= 1)
            {
                _zombieTargeter = hit.collider.gameObject.GetComponent<ZombieTargeter>();
                if (_zombieTargeter.LevelAttentive == 3)
                {
                    RandomTarget(_zombieTargeter.CurrentTarget.transform.position, false);
                    _levelAttective = 2;
                }
                if (_zombieTargeter.LevelAttentive == 2)
                {
                    RandomTarget(_zombieTargeter.CurrentTarget.transform.position, true);
                    _levelAttective = 1;
                }
                else
                {
                    _zombieTargeter = null;
                    _levelAttective = 1;
                }
            }
        }
    }

    private void RandomTarget(Vector3 position, bool spread)
    {
        _ellapsedTime = 0;
        position = new Vector3(
            position.x + (transform.position.x < position.x ? -0.5f : 0.5f), 
            position.y,
            position.z + (transform.position.z < position.z ? -0.5f : 0.5f));
        if (_randomTarget == null)
            _randomTarget = Instantiate(_template, transform.position, transform.rotation);
        _randomTarget.SetPosition(position, spread);
    }
}
