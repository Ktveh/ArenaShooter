using UnityEngine;
using UnityEngine.Events;

public class PlayerZombieKillCounter : MonoBehaviour
{
    [SerializeField] private ZombieCounter _containerZombie;

    private Zombie[] _zombies;

    public int Count { get; private set; }

    public event UnityAction<int> Changed;

    private void Awake()
    {
        _zombies = _containerZombie.GetComponentsInChildren<Zombie>();
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

    public void Recover(int value)
    {
        Count = value;
    }

    private void OnDead(Zombie zombie)
    {
        Changed?.Invoke(++Count);
    }
}
