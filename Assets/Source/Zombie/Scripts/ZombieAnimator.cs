using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string WalkAnimation = "Walk";
    private const string RunAnimation = "Run";
    private const string AttackAnimation = "Attack";
    private const string CrawlAnimation = "Crawl";

    public void Run()
    {
        _animator.SetBool(WalkAnimation, false);
        _animator.SetBool(RunAnimation, true);
    }

    public void Walk()
    {
        _animator.SetBool(WalkAnimation, true);
        _animator.SetBool(RunAnimation, false);
    }

    public void Stop()
    {
        _animator.SetBool(WalkAnimation, false);
        _animator.SetBool(RunAnimation, false);
    }

    public void StartAttack()
    {
        _animator.SetBool(AttackAnimation, true);
    }

    public void StopAttack()
    {
        _animator.SetBool(AttackAnimation, false);
    }

    public void Crawl()
    {
        _animator.SetBool(CrawlAnimation, true);
    }
}
