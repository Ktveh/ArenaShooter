using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class InterfaceHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    
    private Slider _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>(); 
    }

    private void OnEnable()
    {
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
