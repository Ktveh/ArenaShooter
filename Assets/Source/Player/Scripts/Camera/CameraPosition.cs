using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private void LateUpdate()
    {
        transform.position = _point.transform.position;
    }
}
