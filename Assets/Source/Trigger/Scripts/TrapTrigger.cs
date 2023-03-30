using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] _objectsForShow;
    [SerializeField] private Transform[] _objectsForHide;
    [SerializeField] private Sound _sound;
    [SerializeField] private ParticleSystem[] _effects;
    [SerializeField] private bool _moveObjectHere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>())
        {
            Active();
        }
    }

    private void Start()
    {
        foreach (var obj in _objectsForShow)
        {
            obj.gameObject.SetActive(false);
            if (_moveObjectHere)
                obj.position = transform.position;
        }
    }

    private void Active()
    {
        _sound.Play();

        foreach (var effect in _effects)
        {
            effect.Play();
        }

        foreach (var transform in _objectsForShow)
        {
            transform.gameObject.SetActive(true);
        }

        foreach (var transform in _objectsForHide)
        {
            transform.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
