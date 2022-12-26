using UnityEngine;
using UnityEngine.UI;

public class InterfaceScreenReddening : MonoBehaviour
{
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
        _image.gameObject.SetActive(false);
        _image.gameObject.SetActive(true);
    }
}
