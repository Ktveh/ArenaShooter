using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private ZombieCounter _zombieCounter;

    private Zombie[] _zombies;
    private int _value;

    public event UnityAction<int> ChangedValue;

    private void Awake()
    {
        _zombies = _zombieCounter.GetComponentsInChildren<Zombie>();
    }

    private void OnEnable()
    {
        foreach (Zombie zombie in _zombies)
            zombie.Dead += OnDead;
    }

    private void OnDisable()
    {
        foreach (Zombie zombie in _zombies)
            zombie.Dead -= OnDead;
    }

    public void SetValue(int value)
    {
        _value = value;
    }
    
    private void OnDead(Zombie zombie)
    {
        //_value += zombie.Reward;
        _value += 50;
        ChangedValue?.Invoke(_value);
    }
}
