using UnityEngine;
using Cinemachine;
using System.Collections;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraZoom : MonoBehaviour
{
    private const float DefaultFieldOfView = 40f;
    private const float ScopingFieldOfView = 30f;

    [SerializeField] private PlayerScopeOpening _playerScopeOpening;
    [SerializeField] private StarterAssets.FirstPersonController _firstPersonController;
    [SerializeField] private float _step = 0.5f;

    private CinemachineVirtualCamera _camera;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (_firstPersonController.IsRunning)
            _camera.m_Lens.FieldOfView = DefaultFieldOfView;
    }

    private void OnEnable()
    {
        _playerScopeOpening.Scoped += OnScoped;
        _playerScopeOpening.NonScoped += OnNonScoped;
    }

    private void OnDisable()
    {
        _playerScopeOpening.Scoped -= OnScoped;
        _playerScopeOpening.NonScoped -= OnNonScoped;
    }

    private void OnScoped()
    {
        if (_firstPersonController.IsRunning == false)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(Change(ScopingFieldOfView));
        }
    }
    
    private void OnNonScoped()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(Change(DefaultFieldOfView - _step));
    }

    private IEnumerator Change(float value)
    {
        if (value == DefaultFieldOfView - _step)
        {
            while (_camera.m_Lens.FieldOfView <= value)
            {
                _camera.m_Lens.FieldOfView += _step;
                yield return null;
            }
        }
        else if(value == ScopingFieldOfView)
        {
            while (_camera.m_Lens.FieldOfView >= value)
            {
                _camera.m_Lens.FieldOfView -= _step;
                yield return null;
            }
        }
    }
}
