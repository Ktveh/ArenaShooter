using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private void LateUpdate()
    {
        transform.position = _playerMovement.transform.position;
    }
}
