using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private SoundTarget _template;
    [SerializeField] private float _radius;
    [SerializeField] private float _duration;

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
        SoundTarget soundTarget = Instantiate(_template, transform.position, transform.rotation);
        soundTarget.SetRadius(_radius);
        soundTarget.SetDuration(_duration);
    }
}
