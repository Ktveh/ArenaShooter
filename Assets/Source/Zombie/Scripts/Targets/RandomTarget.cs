using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTarget : Target
{
    [SerializeField] private float _spreadPosition;

    public void SetPosition(Vector3 position, bool spread)
    {
        if (spread)
        {
            transform.position = new Vector3(
                position.x + Random.Range(-_spreadPosition, _spreadPosition), 
                position.y, 
                position.z + Random.Range(-_spreadPosition, _spreadPosition)
                );
        }
        else
        {
            transform.position = position;
        }
    } 
}
