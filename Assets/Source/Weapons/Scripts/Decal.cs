using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Decal : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private ParticleSystem _particleSystem;
    private float _duartion => _particleSystem.main.startLifetimeMultiplier - 0.2f;

    private void OnEnable()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        Invoke(nameof(Return), _duartion);
    }

    public void Set(Transform parent)
    {
        transform.SetParent(parent);
    }

    private void Return()
    {
        transform.parent = _container;
    }
}
