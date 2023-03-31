using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform[] _positions;

    private void Awake()
    {
        if (_positions.Length > 0)
        {
            _player.position = _positions[Random.Range(0, _positions.Length)].position;
        }
    }
}
