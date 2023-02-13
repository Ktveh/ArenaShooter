using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTarget : Target
{
    [SerializeField] private SphereCollider _collider;

    private float _duration;

    private void Update()
    {
        _duration -= Time.deltaTime;
        if (_duration <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetRadius(float radius)
    {
        _collider.radius = radius;
    }

    public void SetDuration(float duration)
    {
        _duration = duration;
    }
}
