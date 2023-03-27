using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraZoom : MonoBehaviour
{
    private const float DefaultFieldOfView = 40f;
    private const float ScopingFieldOfView = 30f;

    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private float _speed = 40f;

    private CinemachineVirtualCamera _camera;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (_playerWeaponSelecting.CurrentWeapon.IsScoping)
        {
            if(_camera.m_Lens.FieldOfView >= ScopingFieldOfView)
            {
                _camera.m_Lens.FieldOfView -= _speed * Time.deltaTime;
            }
        }
        else
        {
            if (_camera.m_Lens.FieldOfView <= DefaultFieldOfView)
            {
                _camera.m_Lens.FieldOfView += _speed * Time.deltaTime;
            }
        }
    }
}
