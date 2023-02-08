using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _damageWithoutArms;
    [SerializeField] private int _price;
    [SerializeField] private float _delayAfterDead;
    [SerializeField] private float _delayBetweenAttacks;
    [SerializeField] private ZombieAnimator _animator;
    [SerializeField] private ZombieTargeter _targeter;
    [SerializeField] private ZombieMover _mover;
    [SerializeField] private ParticleSystem _deadEffect;
    [SerializeField] private Sound _sound;

    private PlayerHealth _playerHealth;
    private bool _hasLegs = true;
    private bool _hasArms = true;
    private bool _isDead = false;
    private float _ellapsedTime = 0;

    public event UnityAction<Zombie> Dead;

    public bool HasLegs => _hasLegs;
    public bool IsDead => _isDead;
    public int Health => _health;
    public int Price => _price;

    private void Update()
    {
        _ellapsedTime += Time.deltaTime;
        if (_isDead)
        {
            if (_ellapsedTime > _delayAfterDead)
            {
                HideBody();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>())
        {
            _playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (!_isDead && _ellapsedTime > _delayBetweenAttacks)
            {
                Attack();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>())
        {
            StopAttack();
        }
    }

    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
            else
            {
                _targeter.RemoveCurrentTarget();
            }
        }
    }

    public void RemoveLeg()
    {
        _hasLegs = false;
    }

    public void RemoveArm()
    {
        _hasArms = false;
    }

    private void Die()
    {
        _ellapsedTime = 0;
        Dead?.Invoke(this);
        _isDead = true;
        _animator.Dead();
    }

    private void Attack()
    {
        _ellapsedTime = 0;
        _mover.StopMove();
        if (_hasArms)
        {
            _playerHealth.Take(_damage);
        }
        else
        {
            _playerHealth.Take(_damageWithoutArms);
        }
        _sound.Play();
        _animator.StartAttack();
    }

    private void StopAttack()
    {
        _mover.StartMove();
        _animator.StopAttack();
    }

    private void HideBody()
    {
        gameObject.SetActive(false);
    }
}
