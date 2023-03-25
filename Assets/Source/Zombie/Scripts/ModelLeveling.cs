using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLeveling : MonoBehaviour
{
    private void Update()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = new Quaternion(0,0,0,0);
    }
}
