using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceDeadZombieMarker : MonoBehaviour
{
    [SerializeField] private ZombieCounter _containerZombie;
    [SerializeField] private Image _dead;
    [SerializeField] private float _delay;

    private Coroutine _currentCoroutine;
    private Zombie[] _zombies;

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
        _dead.gameObject.SetActive(true);

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(_delay);
        _dead.gameObject.SetActive(false);
    }
}
