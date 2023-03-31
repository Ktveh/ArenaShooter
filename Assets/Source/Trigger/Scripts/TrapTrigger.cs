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

        foreach (var obj in _objectsForShow)
        {
            obj.gameObject.SetActive(true);
        }

        foreach (var obj in _objectsForHide)
        {
            obj.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
