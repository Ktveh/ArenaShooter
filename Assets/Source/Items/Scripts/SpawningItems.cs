using System.Collections.Generic;
using UnityEngine;

public class SpawningItems : MonoBehaviour
{
    [SerializeField] private Transform _weaponRespawningContainer;
    [SerializeField] private Transform _drugRespawningContainer;
    [SerializeField] private Transform _grenadeRespawningContainer;
    [SerializeField] private Item[] _weapons;
    [SerializeField] private Item _drug;
    [SerializeField] private Item[] _grenades;
    [SerializeField] private int AmountWeapon;
    [SerializeField] private int AmountDrug;
    [SerializeField] private int _amountGrenade;

    private Transform[] _pointsWeapons;
    private Transform[] _pointsDrugs;
    private Transform[] _pointsGrenades;

    private void Awake()
    {
        _pointsWeapons = _weaponRespawningContainer.GetComponentsInChildren<Transform>();
        _pointsDrugs = _drugRespawningContainer.GetComponentsInChildren<Transform>();
        _pointsGrenades = _grenadeRespawningContainer.GetComponentsInChildren<Transform>();

        foreach (Transform point in _pointsWeapons)
            point.eulerAngles = new Vector3(0, point.eulerAngles.y);

        foreach (Transform point in _pointsGrenades)
            point.eulerAngles = new Vector3(0, point.eulerAngles.y);

        if (AmountWeapon > _pointsWeapons.Length)
            AmountWeapon = _pointsWeapons.Length;

        if (AmountDrug > _pointsDrugs.Length)
            AmountDrug = _pointsDrugs.Length;

        if (_amountGrenade > _pointsGrenades.Length)
            _amountGrenade = _pointsGrenades.Length;

        List<int> numbers = new List<int>();
        int number = 0;

        for (int i = 0; i < AmountWeapon; i++)
        {
            while (numbers.Contains(number))
            {
                number = Random.Range(0, _pointsWeapons.Length);
            }

            numbers.Add(number);
            Vector3 position = _pointsWeapons[number].transform.position;
            Quaternion rotation = _pointsWeapons[number].transform.rotation;

            Instantiate(_weapons[Random.Range(0, _weapons.Length)], position, rotation, transform);
        }

        numbers.Clear();
        number = 0;

        for (int i = 0; i < AmountDrug; i++)
        {
            while (numbers.Contains(number))
            {
                number = Random.Range(0, _pointsDrugs.Length);
            }
            
            numbers.Add(number);
            Vector3 position = _pointsDrugs[number].transform.position;
            Quaternion rotation = _pointsDrugs[number].transform.rotation;

            Instantiate(_drug, position, rotation, transform);
        }

        numbers.Clear();
        number = 0;

        for (int i = 0; i < _amountGrenade; i++)
        {
            while (numbers.Contains(number))
            {
                number = Random.Range(0, _pointsGrenades.Length);
            }

            numbers.Add(number);
            Vector3 position = _pointsGrenades[number].transform.position;
            Quaternion rotation = _pointsGrenades[number].transform.rotation;

            Instantiate(_grenades[Random.Range(0, _grenades.Length)], position, rotation, transform);
        }
    }
}
