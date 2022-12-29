using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private ZombieAnimator _animator;
    [SerializeField] private ParticleSystem _deadEffect;
    [SerializeField] private Sound _sound;

    private PlayerHealth _playerHealth;
    private bool _hasLegs = true;
    private bool _isDead = false;

    public event UnityAction<Zombie> Dead;

    public bool HasLegs => _hasLegs;
    public bool IsDead => _isDead;
    public int Health => _health;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>())
        {
            _playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (!_isDead)
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
            _deadEffect.Play();
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }
    }

    public void RemoveLeg()
    {
        _hasLegs = false;
    }

    private void Die()
    {
        Dead?.Invoke(this);
        _isDead = true;
        _animator.Dead();
        //Destroy(gameObject);
    }

    private void Attack()
    {
        _playerHealth.Take(_damage);
        _sound.Play();
        _animator.StartAttack();
    }

    private void StopAttack()
    {
        _animator.StopAttack();
    }
}
