using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTarget : Target
{
    private float _duration;

    private void Update()
    {
        _duration -= Time.deltaTime;
        if (_duration <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetPosition(Vector3 position, float spread)
    {
        transform.position = new Vector3(position.x + Random.Range(-spread, spread), position.y, position.z + Random.Range(-spread, spread));
    }

    public void SetDuration(float duration)
    {
        _duration = duration;
    }
}
