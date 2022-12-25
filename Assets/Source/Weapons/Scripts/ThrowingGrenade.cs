using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WeaponAnimator))]
public class ThrowingGrenade : MonoBehaviour
{
    private const float _grenadeSpawnDelay = 0.35f;

    [SerializeField] private GameObject _prefabs;
    [SerializeField] private Transform _spawningPoint;

    private WeaponAnimator _weaponAnimator;

    private void Awake()
    {
        _weaponAnimator = GetComponent<WeaponAnimator>();
        enabled = false;
    }

    private void OnEnable()
    {
        StartCoroutine(GrenadeSpawnDelay());
        _weaponAnimator.ThrowGrenade();
    }

    private IEnumerator GrenadeSpawnDelay()
    {
        yield return new WaitForSeconds(_grenadeSpawnDelay);

        Instantiate(_prefabs, _spawningPoint.position, _spawningPoint.transform.rotation);
        enabled = false;
    }
}
