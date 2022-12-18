using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTargeter : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _angleDetected;
    [SerializeField] private float _distanceDetected;
    [SerializeField] private int _amountRaycasts;
    [SerializeField] private ZombieTarget _ownTarget;
    [SerializeField] private RandomTarget _template;

    private ZombieTarget _zombieTarget;
    private SoundTarget _soundTarget;
    private MainTarget _mainTarget;
    private RandomTarget _randomTarget;
    private Target _currentTarget;

    public Target CurrentTarget => _currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SoundTarget>())
        {
            _soundTarget = other.GetComponent<SoundTarget>();
            _randomTarget = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SoundTarget>())
        {
            _soundTarget = null;
        }
    }

    private void Update()
    {
        FoundTargets();
        SetCurrentTarget();
    }

    private void FoundTargets()
    {
        float angle = 0;
        _mainTarget = null;
        _zombieTarget = null;
        _ownTarget.IsAction = false;

        for (int i = 0; i < _amountRaycasts; i++)
        {
            float x = Mathf.Sin(angle);
            float z = Mathf.Cos(angle);

            angle += _angleDetected * Mathf.Deg2Rad / _amountRaycasts;

            GetRaycast(transform.TransformDirection(new Vector3(x, 0, z)));

            if (x != 0)
            {
                GetRaycast(transform.TransformDirection(new Vector3(-x, 0, z)));
            }
        }
    }

    private void SetCurrentTarget()
    {
        if (_mainTarget != null)
        {
            _currentTarget = _mainTarget;
        }
        else if (_zombieTarget != null)
        {
            _currentTarget = _zombieTarget;
        }
        else if (_soundTarget != null)
        {
            _currentTarget = _soundTarget;
        }
        else if (_randomTarget != null)
        {
            _currentTarget = _randomTarget;
        }
        else
        {
            RandomTarget();
        }
    }

    private void GetRaycast(Vector3 direction)
    {
        Vector3 startPosition = transform.position + _offset;
        RaycastHit hit = new RaycastHit();
        
        if (Physics.Raycast(startPosition, direction, out hit, _distanceDetected))
        {   
            if (hit.collider.gameObject.GetComponent<MainTarget>())
            {
                _mainTarget = hit.collider.gameObject.GetComponent<MainTarget>();
                if (_randomTarget != null)
                {
                    SettingRandomTarget(_mainTarget.transform.position, 0, 5);
                }
                _ownTarget.IsAction = true;
            }
            else if (hit.collider.gameObject.GetComponent<ZombieTarget>())
            {
                _zombieTarget = hit.collider.gameObject.GetComponent<ZombieTarget>();
                if (_zombieTarget.IsAction)
                {
                    if (_randomTarget != null)
                    {
                        SettingRandomTarget(_zombieTarget.transform.position, 0, 5);
                    }
                }
                else
                {
                    _zombieTarget = null;
                }
            }
        }
    }

    private void RandomTarget()
    {
        _randomTarget = Instantiate(_template, transform.position, transform.rotation);
        SettingRandomTarget(transform.position, 5, 5);
    }

    private void SettingRandomTarget(Vector3 position, float spread, float duration)
    {
        _randomTarget.SetPosition(position, spread);
        _randomTarget.SetDuration(duration);
    }
}
