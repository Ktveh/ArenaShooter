using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMover : MonoBehaviour
{
    [SerializeField] private Zombie _zombie;
    [SerializeField] private ZombieTargeter _targeter;
    [SerializeField] private ZombieAnimator _animator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _crawlSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [Range(0,1)]
    [SerializeField] private float _spreadSpeed;

    private Target _target;

    private void Update()
    {
        if (_target != null)
        {
            _agent.destination = _target.transform.position;
            if (_target is MainTarget && _zombie.HasLegs && !_zombie.IsDead)
            {
                _agent.speed = SetSpeed(_runSpeed);
                _animator.Run();
            }
            else if (_zombie.HasLegs && !_zombie.IsDead)
            {
                _agent.speed = SetSpeed(_walkSpeed);
                _animator.Walk();
            }
            else if (!_zombie.IsDead)
            {
                _agent.speed = SetSpeed(_crawlSpeed);
                _animator.Crawl();
            }
            else if (_zombie.IsDead)
            {
                _agent.speed = SetSpeed(0);
            }
        }
        if (!_agent.hasPath)
        {
            _animator.Stop();
        }
        _target = _targeter.CurrentTarget;
    }

    private float SetSpeed(float speed)
    {
        return speed += speed * Random.Range(_spreadSpeed, -_spreadSpeed);
    }
}
