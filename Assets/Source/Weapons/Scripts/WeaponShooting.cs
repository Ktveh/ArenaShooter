using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponAnimator))]
[RequireComponent(typeof(WeaponSound))]
[RequireComponent(typeof(WeaponParticles))]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(WeaponShowingBulletCase))]
[RequireComponent(typeof(ShowingBulletDecals))]
public class WeaponShooting : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
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
    private ShowingBulletDecals _showingBulletDecals;
    private bool _isHited;

    private float randomNumber => Random.Range(_minRandomDirection, _maxRandomDirection);

    public event UnityAction Shooted;
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
        _showingBulletDecals = GetComponent<ShowingBulletDecals>();
    }

    public void LaunchBullet(bool isScoping)
    {
        Shooted?.Invoke();
        _weaponShowingBulletCase.enabled = true;
        _weaponAnimator.Fire(isScoping);
        _weaponSound.Fire();
        _weaponParticles.enabled = true;
        Transform camera = Camera.main.transform;
        RaycastHit hit;

        if (_weapon.Type != Weapon.Types.SniperRifle && _weapon.Type != Weapon.Types.Shotgun)
        {
            _isHited = false;

            if (Physics.Raycast(camera.position, camera.forward, out hit, _maxDistance, _layerMask))
                MakeDamage(hit);
        }
        else if (_weapon.Type == Weapon.Types.SniperRifle)
        {
            _isHited = false;

            RaycastHit[] hits;
            hits = Physics.RaycastAll(camera.position, camera.forward, _maxDistance, _layerMask);
            int amount = hits.Length < _maximumNumberTargetsHit ? hits.Length : _maximumNumberTargetsHit;

            for (int i = hits.Length - 1; i > hits.Length - 1 - amount; i--)
                MakeDamage(hits[i]);
        }
        else if (_weapon.Type == Weapon.Types.Shotgun)
        {
            _isHited = false;

            for (int i = 0; i < _amountBullets; i++)
            {
                Vector3 randomDirection = new Vector3(randomNumber, randomNumber, randomNumber);
                Vector3 direction = camera.forward + randomDirection;

                if (Physics.Raycast(camera.position, direction, out hit, _maxDistance, _layerMask))
                    MakeDamage(hit);
            }
        }
    }

    private void MakeDamage(RaycastHit hit)
    {
        _showingBulletDecals.Show(hit);

        if (hit.collider.TryGetComponent(out Zombie zombie))
        {
            zombie.TakeDamage(_damage);
            if (_isHited == false)
            {
                Hited?.Invoke();
                _isHited = true;
            }
        }
        else if (hit.collider.TryGetComponent(out ZombieLimb zombieLimb))
        {
            zombieLimb.TakeDamage(_damage);

            if (_isHited == false)
            {
                if (zombieLimb.IsHead)
                    HitedInHead?.Invoke();
                else
                    Hited?.Invoke();

                _isHited = true;
            }
        }
    }
}
