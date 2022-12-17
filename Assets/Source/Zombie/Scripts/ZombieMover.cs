using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMover : MonoBehaviour
{
    [SerializeField] private ZombieTargeter _targeter;
    [SerializeField] private ZombieAnimator _animator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _spreadSpeed;

    private Target _target;

    private void Update()
    {
        if (_target != null)
        {
            _agent.destination = _target.transform.position;
            if (_target is MainTarget || _target is ZombieTarget || _target is SoundTarget)
            {
                _agent.speed = _maxSpeed + Random.Range(-_spreadSpeed, _spreadSpeed);
                _animator.Run();
            }
            else
            {
                _agent.speed = _minSpeed + Random.Range(-_spreadSpeed, _spreadSpeed);
                _animator.Walk();
            }
        }
        if (!_agent.hasPath)
        {
            _animator.Stop();
        }
        _target = _targeter.CurrentTarget;

    }
}
