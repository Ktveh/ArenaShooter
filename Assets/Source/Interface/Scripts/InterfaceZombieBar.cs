using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class InterfaceZombieBar : MonoBehaviour
{
    [SerializeField] private ZombieCounter _zombieCounter;

    private Zombie[] _zombies;
    private Slider _slider;
    private TMP_Text _showingAmount;
    private int _amountDead;

    public event UnityAction AllZombiesDead;

    private void Awake()
    {
        _zombies = _zombieCounter.GetComponentsInChildren<Zombie>();
        _slider = GetComponent<Slider>();
        _showingAmount = GetComponentInChildren<TMP_Text>();
        _slider.maxValue = _zombies.Length;
        _showingAmount.text = _amountDead.ToString() + " / " + _slider.maxValue.ToString();
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
        _slider.value = ++_amountDead;
        _showingAmount.text = _amountDead.ToString() + " / " + _slider.maxValue.ToString();

        if (_slider.value == _slider.maxValue)
            AllZombiesDead?.Invoke();
    }
}
