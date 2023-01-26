using System.Linq;
using UnityEngine;

public class WeaponShowingBulletCase : MonoBehaviour
{
    [SerializeField] private Transform _containerBulletCase;
    [SerializeField] private Transform _pointSpawnBulletCase;
    private BulletCase[] _bulletsCases;

    private void Awake()
    {
        _bulletsCases = _containerBulletCase.GetComponentsInChildren<BulletCase>();

        foreach (BulletCase bulletCase in _bulletsCases)
        {
            Debug.Log(bulletCase.gameObject);
            bulletCase.gameObject.SetActive(false);
        }
    }

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
