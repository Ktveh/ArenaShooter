using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private SoundTarget _template;
    [SerializeField] private float _radius;
    [SerializeField] private float _duration;

    SoundTarget _soundTarget;

    public void Play()
    {
        foreach (var source in _audioSources)
        {
            if (source.isPlaying)
            {
                return;
            }
        }

        int index = Random.Range(0, _audioSources.Length);
        _audioSources[index].Play();
        SetSoundTarget();
    }

    private void SetSoundTarget()
    {
        if (_soundTarget == null)
        {
            _soundTarget = Instantiate(_template, transform.position, transform.rotation);
            ChangeSoundTarget();
        }
        else
        {
            _soundTarget.transform.position = transform.position;
            ChangeSoundTarget();
            _soundTarget.gameObject.SetActive(true);
        }
    }

    private void ChangeSoundTarget()
    {
        _soundTarget.SetRadius(_radius);
        _soundTarget.SetDuration(_duration);
    }
}
