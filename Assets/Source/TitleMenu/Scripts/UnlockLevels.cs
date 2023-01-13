using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevels : MonoBehaviour
{
    [SerializeField] private Level[] _levels;

    public void UnlockAll()
    {
        foreach(Level level in _levels)
        {
            level.Unlock();
        }
    }
}
