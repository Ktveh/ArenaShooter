using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleterTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<DeleteObject>())
            other.gameObject.SetActive(false);
    }
}
