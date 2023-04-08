using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private RewritingLocalSave _localSave;
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
        Unlock();
    }

    private void OnEnable()
    {
        _localSave.Rewrited += Unlock;
    }

    private void OnDisable()
    {
        _localSave.Rewrited -= Unlock;
    }

    public void Unlock()
    {
        if (_scene <= PlayerPrefs.GetInt(SaveKey))
        {
            _isLock = false;
        }
    }
}
