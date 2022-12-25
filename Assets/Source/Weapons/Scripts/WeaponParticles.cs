using System.Linq;
using UnityEngine;

[RequireComponent(typeof(WeaponAccessories))]
public class WeaponParticles : MonoBehaviour
{
    [SerializeField] private Transform _silencerParticleContainer;
    [SerializeField] private Transform _nonSilencerParticleContainer;

    private WeaponAccessories _weaponAccessories;
    private WeaponParticle[] _silencerParticle;
    private WeaponParticle[] _nonSilencerParticle;
    private WeaponParticle _particle;

    private void Awake()
    {
        _weaponAccessories = GetComponent<WeaponAccessories>();
        _silencerParticle = _silencerParticleContainer.GetComponentsInChildren<WeaponParticle>();
        _nonSilencerParticle = _nonSilencerParticleContainer.GetComponentsInChildren<WeaponParticle>();

        foreach (var particle in _silencerParticle)
            particle.gameObject.SetActive(false);
        
        foreach (var particle in _nonSilencerParticle)
            particle.gameObject.SetActive(false);

        enabled = false;
    }

    private void OnEnable()
    {
        if (_weaponAccessories.IsEnabledSilencer)
            _particle = _silencerParticle.FirstOrDefault(particle => particle.gameObject.activeSelf == false);
        else
            _particle = _nonSilencerParticle.FirstOrDefault(particle => particle.gameObject.activeSelf == false);
        
        if(_particle != null)
            _particle.gameObject.SetActive(true);

        enabled = false;
    }
}
