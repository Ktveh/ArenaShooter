using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private ZombieAnimator _animator;

    private bool _hasLegs;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<MainTarget>())
        {
            Attack();
        }
    }

    public void TakeDamage(float damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }
    }

    private void Update()
    {
        if (!_hasLegs)
        {
            _animator.Crawl();
        }
    }

    private void Die()
    {

    }

    private void Attack()
    {
        _animator.Attack();
    }
}
