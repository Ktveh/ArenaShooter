using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevels : MonoBehaviour
{
    [SerializeField] private Level[] _levels;

    private string SaveKey = "Levels";

    public void UnlockAll()
    {
        foreach(Level level in _levels)
        {
            level.Unlock();
        }
        PlayerPrefs.SetInt(SaveKey, 6);
    }
}
