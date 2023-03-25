using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(WeaponAnimator))]
public class ThrowingGrenade : MonoBehaviour
{
    private const float _grenadeSpawnDelay = 0.35f;

    [SerializeField] private Transform _spawningPoint;
    [SerializeField] private Rigidbody[] _grenades;
    [SerializeField] private float _speedGrenade;

    private WeaponAnimator _weaponAnimator;

    public bool IsThrows { get; private set; }

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
        IsThrows = true;
        yield return new WaitForSeconds(_grenadeSpawnDelay);

        Throw();
        enabled = false;
        IsThrows = false;
    }

    private void Throw()
    {
        Rigidbody grenade = _grenades.FirstOrDefault(grenade => grenade.gameObject.activeSelf == false);
        Vector3 speed = transform.forward * _speedGrenade * Time.deltaTime;

        if (grenade != null)
        {
            grenade.transform.eulerAngles = _spawningPoint.eulerAngles;
            grenade.transform.position = _spawningPoint.position;
            grenade.gameObject.SetActive(true);
            grenade.AddForce(speed, ForceMode.VelocityChange);
        }
    }
}
