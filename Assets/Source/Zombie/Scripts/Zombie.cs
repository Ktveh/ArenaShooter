using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private ZombieAnimator _animator;
    [SerializeField] private ParticleSystem _deadEffect;
    [SerializeField] private Sound _sound;

    private bool _hasLegs = true;

    public event UnityAction<Zombie> Dead;

    public bool HasLegs => _hasLegs;
    public int Health => _health;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MainTarget>())
        {
            Attack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MainTarget>())
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
        Destroy(gameObject);
    }

    private void Attack()
    {
        _sound.Play();
        _animator.StartAttack();
    }

    private void StopAttack()
    {
        _animator.StopAttack();
    }
}
