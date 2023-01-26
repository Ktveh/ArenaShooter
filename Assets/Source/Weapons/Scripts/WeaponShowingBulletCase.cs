using System.Linq;
using UnityEngine;

public class WeaponShowingBulletCase : MonoBehaviour
{
    [SerializeField] private Transform _pointSpawnBulletCase;
    [SerializeField] private BulletCase[] _bulletsCases;

    private void OnEnable()
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
