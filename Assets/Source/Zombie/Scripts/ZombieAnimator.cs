using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _minSpeedAnimation;
    [SerializeField] private float _maxSpeedAnimation;

    private const string WalkAnimation = "Walk";
    private const string RunAnimation = "Run";
    private const string AttackAnimation = "Attack";
    private const string CrawlAnimation = "Crawl";
    private const string DeadAnimation = "Dead";

    private void Start()
    {
        SetSpeedAnimation();
    }

    public void Run()
    {
        _animator.SetBool(WalkAnimation, false);
        _animator.SetBool(RunAnimation, true);
        SetSpeedAnimation();
    }

    public void Walk()
    {
        _animator.SetBool(WalkAnimation, true);
        _animator.SetBool(RunAnimation, false);
        SetSpeedAnimation();
    }

    public void Stop()
    {
        _animator.SetBool(WalkAnimation, false);
        _animator.SetBool(RunAnimation, false);
        SetSpeedAnimation();
    }

    public void StartAttack()
    {
        _animator.SetBool(AttackAnimation, true);
        SetSpeedAnimation();
    }

    public void StopAttack()
    {
        _animator.SetBool(AttackAnimation, false);
    }

    public void Crawl()
    {
        _animator.SetBool(CrawlAnimation, true);
        SetSpeedAnimation();
    }

    public void Dead()
    {
        _animator.SetBool(DeadAnimation, true);
        SetSpeedAnimation();
    }
    
    private void SetSpeedAnimation()
    {
        _animator.speed = Random.Range(_minSpeedAnimation, _maxSpeedAnimation);
    }
}
