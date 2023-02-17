using UnityEngine;
using UnityEngine.UI;

public class InterfaceScreenReddening : MonoBehaviour
{
    private const float Delay = 0.2f;

    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Image _image;

    private void OnEnable()
    {
        _playerHealth.TookDamage += OnTookDamage;
    }

    private void OnDisable()
    {
        _playerHealth.TookDamage -= OnTookDamage;
    }

    private void OnTookDamage()
    {
        _image.gameObject.SetActive(true);
        Invoke(nameof(Disable), Delay);
    }

    private void Disable()
    {
        _image.gameObject.SetActive(false);
    }
}
