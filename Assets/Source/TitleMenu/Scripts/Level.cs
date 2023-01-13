using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _scene;
    [SerializeField] private bool _isLock;

    public string Name => _name;
    public int Scene => _scene;
    public bool IsLock => _isLock;

    public void Unlock()
    {
        _isLock = false;
    }
}
