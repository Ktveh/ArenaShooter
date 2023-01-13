using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTarget : Target
{
    [SerializeField] private float _spreadPosition;
    [SerializeField] private float _maxDuration;

    private float _duration;

    private void Update()
    {
        _duration -= Time.deltaTime;
        if (_duration <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetPosition(Vector3 position, bool spread)
    {
        if (spread)
        {
            transform.position = new Vector3(position.x + Random.Range(-_spreadPosition, _spreadPosition), position.y, position.z + Random.Range(-_spreadPosition, _spreadPosition));
        }
        else
        {
            transform.position = position;
        }
    } 

    public void SetDuration(float duration)
    {
        _duration = duration;
    }

    public void SetDuration()
    {
        _duration = Random.Range(0, _maxDuration);
    }
}
