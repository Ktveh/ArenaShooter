using UnityEngine;
using System.Linq;
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
    [Header("GrenadeLauncher settings")]
    [SerializeField] private Transform _bulletLaunchPoint;
    [SerializeField] private Rigidbody[] _bullets;
    [SerializeField] private float _speedGrenade;

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

    private void Update()
    {
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 1000, Color.green, 0.1f, false);
    }

    public void LaunchBullet(bool isScoping)
    {
        Shooted?.Invoke();
        _weaponShowingBulletCase.enabled = true;
        _weaponAnimator.Fire(isScoping);
        _weaponSound.Fire();
        _weaponParticles.Show();
        Transform camera = Camera.main.transform;
        RaycastHit hit;

        if (_weapon.Type == Weapon.Types.Shotgun)
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
        else if (_weapon.Type == Weapon.Types.GrenadeLauncher)
        {
            _isHited = false;
            Rigidbody bullet = _bullets.FirstOrDefault(bullet => bullet.gameObject.activeSelf == false);
            Vector3 speed = transform.forward * _speedGrenade * Time.deltaTime;

            if (bullet != null)
            {
                bullet.transform.eulerAngles = _bulletLaunchPoint.eulerAngles;
                bullet.transform.position = _bulletLaunchPoint.position;
                bullet.gameObject.SetActive(true);
                bullet.AddForce(speed, ForceMode.VelocityChange);
            }
        }
        else
        {
            _isHited = false;

            if (Physics.Raycast(camera.position, camera.forward, out hit, _maxDistance, _layerMask))
                MakeDamage(hit);
        }
    }

    public void MakeDamage(RaycastHit hit, float distance = 1, int damage = 0, bool isHandeGrenade = false)
    {
        damage = isHandeGrenade ? damage : _damage;

        if (_weapon.Type == Weapon.Types.GrenadeLauncher || isHandeGrenade)
        {
            distance = distance < 1 ? 1 : distance;
            damage = damage / (int)distance;
        }
        else
        {
            _showingBulletDecals.Show(hit);
        }

        if (hit.collider.TryGetComponent(out Zombie zombie))
        {
            zombie.TakeDamage(damage);

            if (_isHited == false)
            {
                if (isHandeGrenade == false)
                {
                    Hited?.Invoke();
                    _isHited = true;
                }
            }
        }
        else if (hit.collider.TryGetComponent(out ZombieLimb zombieLimb))
        {
            zombieLimb.TakeDamage(damage);

            if (_isHited == false)
            {
                if (isHandeGrenade == false)
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
}
