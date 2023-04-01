using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSkin : MonoBehaviour
{
    private const string AmountKilledZombie = "AmountKilledZombie";

    [SerializeField] private List<Material> _materials;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private bool _hasBonusMaterials;
    [SerializeField] private Material _bronze;
    [SerializeField] private Material _silver;
    [SerializeField] private Material _gold;

    private List<Material> _bonusMaterials = new List<Material>();

    private void Awake()
    {
        if (_materials.Count <= 0)
            return;

        if (_hasBonusMaterials)
        {
            if (PlayerPrefs.GetInt(AmountKilledZombie) > 1000)
                _bonusMaterials.Add(_bronze);
            if (PlayerPrefs.GetInt(AmountKilledZombie) > 5000)
                _bonusMaterials.Add(_silver);
            if (PlayerPrefs.GetInt(AmountKilledZombie) > 10000)
                _bonusMaterials.Add(_gold);
        }

        if (Random.Range(0, 100) == 0 && _hasBonusMaterials && _bonusMaterials.Count > 0)
            _renderer.material = _bonusMaterials[Random.Range(0, _bonusMaterials.Count)];
        else
            _renderer.material = _materials[Random.Range(0, _materials.Count)];
    }
}
