using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CreatingSoundTarget))]
[RequireComponent(typeof(WeaponAccessories))]
public class WeaponSound : MonoBehaviour
{
    [SerializeField] private Transform _containerShootingNonSilencer;
    [SerializeField] private Transform _containerShootingSilencer;
    [SerializeField] private AudioSource _aiming;
    [SerializeField] private AudioSource _hidingWeapon;
    [SerializeField] private AudioSource _takingOutWeapon;
    [SerializeField] private AudioSource _reloadingOutOfAmmo;
    [SerializeField] private AudioSource _reloadingAmmoLeft;
    [SerializeField] private AudioSource _shutterSound;

    private CreatingSoundTarget _weaponCreatingSoundTarget;
    private WeaponAccessories _weaponAccessories;
    private AudioSource[] _nonSilencedShots;
    private AudioSource[] _silencedShots;

    private void Start()
    {
        _weaponCreatingSoundTarget = GetComponent<CreatingSoundTarget>();
        _weaponAccessories = GetComponent<WeaponAccessories>();
        _nonSilencedShots = _containerShootingNonSilencer.GetComponentsInChildren<AudioSource>();
        _silencedShots = _containerShootingSilencer.GetComponentsInChildren<AudioSource>();
    }

    public void Aim(bool isScoping)
    {
        if (isScoping)
        {
            if (_aiming.isPlaying == false)
                _aiming.Play();
        }
        else
        {
            _aiming.Stop();
        }
    }

    public void Fire()
    {
        if (_weaponAccessories.IsEnabledSilencer)
        {
            AudioSource sound = _silencedShots.FirstOrDefault(sound => sound.isPlaying == false);

            if (sound != null)
                sound.Play();
        }
        else
        {
            AudioSource sound = _nonSilencedShots.FirstOrDefault(sound => sound.isPlaying == false);
            _weaponCreatingSoundTarget.enabled = true;

            if (sound != null)
                sound.Play();
        }

        _shutterSound.Play();
    }

    public void Hide(bool isHiding)
    {
        if (isHiding)
            _hidingWeapon.Play();
        else
            _takingOutWeapon.Play();
    }

    public void Reload(bool isOutOfAmmo)
    {
        if (isOutOfAmmo)
            _reloadingOutOfAmmo.Play();
        else
            _reloadingAmmoLeft.Play();
    }
}
