using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSkin : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;
    [SerializeField] private Renderer _renderer;

    private void Awake()
    {
        if (_materials.Count > 0)
        {
            _renderer.material = _materials[Random.Range(0, _materials.Count)];
        }
    }
}
