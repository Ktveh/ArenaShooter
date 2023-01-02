using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponAnimator))]
[RequireComponent(typeof(WeaponSound))]
[RequireComponent(typeof(WeaponParticles))]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(WeaponShowingBulletCase))]
public class WeaponShooting : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 1000;
    [SerializeField] private int _damage;
    [Header("Sniper settings")]
    [SerializeField] private int _maximumNumberTargetsHit;
    [Header("Shotgun settings")]
    [SerializeField] private int _amountBullets;
    [SerializeField] private float _maxRandomDirection;
    [SerializeField] private float _minRandomDirection;

    private WeaponAnimator _weaponAnimator;
    private WeaponSound _weaponSound;
    private WeaponParticles _weaponParticles;
    private Weapon _weapon;
    private WeaponShowingBulletCase _weaponShowingBulletCase;

    public event UnityAction Hited;
    public event UnityAction HitedInHead;

    private void Awake()
    {
        _weaponShowingBulletCase = GetComponent<WeaponShowingBulletCase>();
    }

    private void Start()
    {
        _weaponAnimator = GetComponent<WeaponAnimator>();
        _weaponSound = GetComponent<WeaponSound>();
        _weaponParticles = GetComponent<WeaponParticles>();
        _weapon = GetComponent<Weapon>();
    }

    public void LaunchBullet(bool isScoping)
    {
        _weaponShowingBulletCase.enabled = true;
        _weaponAnimator.Fire(isScoping);
        _weaponSound.Fire();
        _weaponParticles.enabled = true;
        Transform camera = Camera.main.transform;
        RaycastHit hit;

        if (_weapon.Type != Weapon.Types.SniperRifle && _weapon.Type != Weapon.Types.Shotgun)
        {
            if (Physics.Raycast(camera.position, camera.forward, out hit, _maxDistance))
                MakeDamage(hit);
        }
        else if (_weapon.Type == Weapon.Types.SniperRifle)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(camera.position, camera.forward, _maxDistance);
            int amount = hits.Length < _maximumNumberTargetsHit ? hits.Length : _maximumNumberTargetsHit;
            
            for (int i = 0; i < amount; i++)
                MakeDamage(hits[i]);
        }
        else if (_weapon.Type == Weapon.Types.Shotgun)
        {
            for (int i = 0; i < _amountBullets; i++)
            {
                Vector3 direction = Camera.main.transform.forward + new Vector3(Random.Range(_minRandomDirection, _maxRandomDirection), Random.Range(_minRandomDirection, _maxRandomDirection));
                if (Physics.Raycast(camera.position, direction, out hit, _maxDistance))
                    MakeDamage(hit);
            }
        }
    }

    private void MakeDamage(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Zombie zombie))
        {
            zombie.TakeDamage(_damage);
            Hited?.Invoke();
        }
        else if (hit.collider.TryGetComponent(out ZombieLimb zombieLimb))
        {
            zombieLimb.TakeDamage(_damage);
            Hited?.Invoke();

            //if (isHead)
            //    HitedInHead?.Invoke();
        }
    }
}
