using System.Linq;
using UnityEngine;

public class WeaponShowingBulletCase : MonoBehaviour
{
    [SerializeField] private Transform _pointSpawnBulletCase;
    [SerializeField] private BulletCase[] _bulletsCases;
    [Header("GrenadeLauncher settings")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _bulletCase;

    private Weapon _weapon;

    private void OnEnable()
    {
        _weapon = GetComponent<Weapon>();

        if (_weapon.Type == Weapon.Types.GrenadeLauncher)
        {
            _bullet.SetActive(true);
            _bulletCase.SetActive(true);
        }
        else
        {
            BulletCase bulletCase = _bulletsCases.FirstOrDefault(bulletCase => bulletCase.gameObject.activeSelf == false);

            if (bulletCase != null)
            {
                bulletCase.SetValuePositionRotation(_pointSpawnBulletCase);
                bulletCase.gameObject.SetActive(true);
            }
            enabled = false;
        }
    }

    private void OnClosed()
    {
        _bullet.SetActive(false);
        _bulletCase.SetActive(false);
        enabled = false;
    }
}
