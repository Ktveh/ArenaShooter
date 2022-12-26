using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class InterfaceHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    
    private Slider _healthBar;

    private void Start()
    {
        _healthBar = GetComponent<Slider>();
        _playerHealth.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _playerHealth.Changed -= OnChanged;
    }

    private void OnChanged(float value)
    {
        _healthBar.value = value;
    }
}
