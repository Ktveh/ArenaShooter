using UnityEngine;

public class PlayerZombieKillCounter : MonoBehaviour
{
    [SerializeField] private ZombieCounter _containerZombie;

    private Zombie[] _zombies;

    public int Count { get; private set; }

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

    private void OnDead(Zombie zombie)
    {
        ++Count;
    }
}
