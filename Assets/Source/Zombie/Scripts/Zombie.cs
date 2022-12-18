using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private ZombieAnimator _animator;
    [SerializeField] private ParticleSystem _deadEffect;
    [SerializeField] private Sound _sound;

    [SerializeField] private bool _hasLegs;

    public bool HasLegs => _hasLegs;

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
            _deadEffect.Play();
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Attack()
    {
        _sound.Play();
        _animator.Attack();
    }
}
