using UnityEngine;
using UnityEngine.UI;

public class InterfaceShowingAiming : MonoBehaviour
{
    [SerializeField] private PlayerScopeOpening _playerScopeOpening;
    [SerializeField] private Image _aim;

    private void OnEnable()
    {
        _playerScopeOpening.Scoped += OnScoped;
        _playerScopeOpening.NonScoped += OnNonScoped;
    }

    private void OnDisable()
    {
        _playerScopeOpening.Scoped -= OnScoped;
        _playerScopeOpening.NonScoped -= OnNonScoped;
    }

    private void OnScoped()
    {
        _aim.gameObject.SetActive(false);
    }

    private void OnNonScoped()
    {
        _aim.gameObject.SetActive(true);
    }
}
