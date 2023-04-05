using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _scene;
    [SerializeField] private bool _isLock;
    [SerializeField] private Sprite _icon;

    public static string SaveKey = "Levels";

    public string Name => _name;
    public int Scene => _scene;
    public bool IsLock => _isLock;
    public Sprite Icon => _icon;

    private void Awake()
    {
        if (_scene <= PlayerPrefs.GetInt(SaveKey))
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        _isLock = false;
    }
}
