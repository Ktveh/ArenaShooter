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

    private float _stopAcceleration = 10f;
    private bool _isMove;
    private Target _target;

    private void Start()
    {
        _isMove = true;
    }

    private void Update()
    {
        if (_target != null)
        {
            _agent.destination = _target.transform.position;

            _agent.stoppingDistance = 0;
            _agent.angularSpeed = 180;
            if (_targeter.LevelAttentive >= 2 && _zombie.HasLegs)
            {
                _agent.speed = SetSpeed(_runSpeed);
                _animator.Run();
                _agent.angularSpeed = 720;
            }
            else if (_zombie.HasLegs)
            {
                _agent.speed = SetSpeed(_walkSpeed);
                _animator.Walk();
            }
            else
            {
                _agent.speed = SetSpeed(_crawlSpeed);
                _animator.Crawl();
            }
        }
        
        _target = _targeter.CurrentTarget;

        if (_zombie.IsDead)
        {
            StopMove();
            return;
        }

        if (!_agent.hasPath)
        {
            if (_targeter.LevelAttentive < 3)
                StopMove();
            else
                _targeter.RemoveCurrentTarget();
            //StartMove();
        }
    }

    public void StopMove()
    {
        _isMove = false;
        _agent.speed = SetSpeed(0);
        _agent.acceleration = _stopAcceleration;
        _animator.Stop();
    }

    public void StartMove()
    {
        _isMove = true;
    }

    private float SetSpeed(float speed)
    {
        return speed += speed * Random.Range(_spreadSpeed, -_spreadSpeed);
    }
}
