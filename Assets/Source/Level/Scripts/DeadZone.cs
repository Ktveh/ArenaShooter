using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>())
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            health.Take(1000);
        }
    }
}
