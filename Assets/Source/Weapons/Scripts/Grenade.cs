using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(CreatingSoundTarget))]
public class Grenade : MonoBehaviour
{
    [SerializeField] private WeaponShooting _weaponShooting;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _radius;
    [SerializeField] private float _delay = 2f;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private CreatingSoundTarget _soundTarget;
    private AudioSource _sound;
    private ParticleSystem[] _particleSystems;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _soundTarget = GetComponent<CreatingSoundTarget>();
        _sound = GetComponentInChildren<AudioSource>();
        _particleSystems = GetComponentsInChildren<ParticleSystem>();
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _meshRenderer.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out SoundTarget soundTarget))
            return;

        _meshRenderer.enabled = false;
        _sound.Play();
        _soundTarget.enabled = true;
        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, _radius, Vector3.forward, _layerMask);

        foreach (RaycastHit hit in raycastHits)
        {
            _weaponShooting.MakeDamage(hit, Vector3.Distance(transform.position, hit.collider.transform.position));
        }

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;

        foreach (ParticleSystem particleSystem in _particleSystems)
        {
            particleSystem.Play();
        }

        Invoke(nameof(Disable), _delay);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
