using System.Linq;
using UnityEngine;

[RequireComponent(typeof(WeaponAnimator))]
[RequireComponent(typeof(WeaponSound))]
[RequireComponent(typeof(WeaponParticles))]
[RequireComponent(typeof(Weapon))]
public class WeaponShooting : MonoBehaviour
{
    [SerializeField] private Transform _containerBulletCase;
    [SerializeField] private Transform _pointSpawnBulletCase;
    [SerializeField] private float _maxDistance = 1000;
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
    private BulletCase[] _bulletsCases;

    private void Awake()
    {
        _bulletsCases = _containerBulletCase.GetComponentsInChildren<BulletCase>();

        foreach (BulletCase bulletCase in _bulletsCases)
            bulletCase.gameObject.SetActive(false);
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
        _weaponAnimator.Fire(isScoping);
        _weaponSound.Fire();
        ShowBulletCase();
        _weaponParticles.enabled = true;
        Transform camera = Camera.main.transform;
        RaycastHit hit;

        if (_weapon.Type != Weapon.Types.SniperRifle && _weapon.Type != Weapon.Types.Shotgun)
        {
            if (Physics.Raycast(camera.position, camera.forward, out hit, _maxDistance))
            {
                //Debug.Log(hit.collider.gameObject);
            }
        }
        else if (_weapon.Type == Weapon.Types.SniperRifle)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(camera.position, camera.forward, _maxDistance);
            int amount = hits.Length < _maximumNumberTargetsHit ? hits.Length : _maximumNumberTargetsHit;

            for (int i = 0; i < amount; i++)
            {
                if (hits[i].collider.TryGetComponent(out Zombie zombie) == false)
                    return;

                Debug.Log(hits[i].collider.gameObject);
            }
        }
        else if (_weapon.Type == Weapon.Types.Shotgun)
        {
            for (int i = 0; i < _amountBullets; i++)
            {
                Vector3 direction = Camera.main.transform.forward + new Vector3(Random.Range(_minRandomDirection, _maxRandomDirection), Random.Range(_minRandomDirection, _maxRandomDirection));
                if (Physics.Raycast(camera.position, direction, out hit, _maxDistance))
                {
                    Debug.Log(hit.collider.gameObject);
                }
            }
        }
    }

    public void ShowBulletCase()
    {
        BulletCase bulletCase = _bulletsCases.FirstOrDefault(bulletCase => bulletCase.gameObject.activeSelf == false);

        if (bulletCase != null)
        {
            bulletCase.SetValuePositionRotation(_pointSpawnBulletCase);
            bulletCase.gameObject.SetActive(true);
        }
    }
}
