using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieCounter : MonoBehaviour
{
    [SerializeField] private int _nextLevel;

    private List<Zombie> _zombies;
    private int _startAmount;

    public int NextLevel => _nextLevel;
    public int StartAmount => _startAmount;

    private void Awake()
    {
        _zombies = new List<Zombie>();
        foreach(Transform child in transform)
        {
            Zombie zombie;
            if (child.GetComponent<Zombie>())
            {
                zombie = child.GetComponent<Zombie>();
                zombie.Dead += ChangeCount;
                _zombies.Add(zombie);
            }
        }
        _startAmount = _zombies.Count;
    }

    public int Amount()
    {
        return _zombies.Count;
    }

    public List<Vector3> GetPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        foreach(Zombie zombie in _zombies)
        {
            if (!zombie.IsDead)
            {
                positions.Add(zombie.transform.position);
            }
        }

        return positions;
    }

    private void ChangeCount(Zombie zombie)
    {
        _zombies.Remove(zombie);
        if (_zombies.Count <= 0)
        {
            if (PlayerPrefs.GetInt(Level.SaveKey) < _nextLevel)
            {
                PlayerPrefs.SetInt(Level.SaveKey, _nextLevel);
            }
        }
    }
}
