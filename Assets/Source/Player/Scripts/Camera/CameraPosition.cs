using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private void Update()
    {
        transform.position = _playerMovement.transform.position;
    }
}
