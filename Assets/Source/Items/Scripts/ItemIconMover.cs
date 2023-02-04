using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconMover : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationDirection;
    [SerializeField] private Vector3 _floatDirection;
    [SerializeField] private float _floatSpeed;

    private Vector3 _startPosition;
    private Vector3 _currentTarget;

    private void Start()
    {
        _startPosition = transform.localPosition;
        _currentTarget = _floatDirection;
    }

    private void Update()
    {
        transform.Rotate(_rotationDirection * Time.deltaTime);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _currentTarget, _floatSpeed * Time.deltaTime);
        if (transform.localPosition == _floatDirection)
        {
            _currentTarget = _startPosition;
        }
        else if (transform.localPosition == _startPosition)
        {
            _currentTarget = _floatDirection;
        }
    }
}
