using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceHitMarker : MonoBehaviour
{
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private Image _body;
    [SerializeField] private Image _head;
    [SerializeField] private float _delay;

    private Coroutine _currentCoroutine;

    private void OnEnable()
    {
        _playerWeaponSelecting.Selected += OnSelected;
    }

    private void OnDisable()
    {
        if (_playerWeaponSelecting.CurrentWeapon.TryGetComponent(out WeaponShooting weaponShooting))
        {
            weaponShooting.Hited -= OnHited;
            weaponShooting.HitedInHead -= OnHitedInHead;
        }

        _playerWeaponSelecting.Selected -= OnSelected;
    }

    private void OnSelected()
    {
        if (_playerWeaponSelecting.CurrentWeapon.TryGetComponent(out WeaponShooting weaponShooting))
        {
            weaponShooting.Hited += OnHited;
            weaponShooting.HitedInHead += OnHitedInHead;
        }
    }

    private void OnHited()
    {
        _body.gameObject.SetActive(true);
        
        if(_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _head.gameObject.SetActive(false);
        }

        _currentCoroutine = StartCoroutine(Hide(_body));
    }
    
    private void OnHitedInHead()
    {
        _head.gameObject.SetActive(true);

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _body.gameObject.SetActive(false);
        }

        _currentCoroutine = StartCoroutine(Hide(_head));
    }

    private IEnumerator Hide(Image image)
    {
        yield return new WaitForSeconds(_delay);
        image.gameObject.SetActive(false);
    }
}
