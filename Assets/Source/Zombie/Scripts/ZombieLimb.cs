using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLimb : MonoBehaviour
{
    [SerializeField] private Zombie _zombie;
    [SerializeField] private int _health;
    [SerializeField] private Transform _limb;
    [SerializeField] private bool _isArm;
    [SerializeField] private bool _isLeg;
    [SerializeField] private bool _isHead;
    [SerializeField] private ParticleSystem _bloodEffect;
    [SerializeField] private ParticleSystem _limbEffect;

    private int _price= 10;
    private int _priceForHead = 50;

    public int Price => _isHead ? _priceForHead : _price;

    public bool IsHead => _isHead;

    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            if (_health <= 0)
            {
                transform.localScale = Vector3.zero;
                _bloodEffect.Play();
                Instantiate(_limbEffect, transform);
                if (_isArm)
                {
                    _zombie.RemoveArm();
                }
                if (_isLeg)
                {
                    _zombie.RemoveLeg();
                }
                if (_isHead)
                {
                    _zombie.TakeDamage(_zombie.Health);
                }
            }
            _zombie.TakeDamage(damage);
        }
    }
}
