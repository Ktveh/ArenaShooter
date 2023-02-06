using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SettingCameraSensitivity : MonoBehaviour
{
    [SerializeField] private StarterAssets.FirstPersonController _firstPersonController;
    [SerializeField] private PlayerSettingSaving _playerSettingSaving;

    private Slider _slider;

    private void Awake()
    {
        if(_playerSettingSaving.CurrentCameraSensitivity != 0)
            _firstPersonController.RotationSpeed = _playerSettingSaving.CurrentCameraSensitivity;
    }

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _slider.value = _firstPersonController.RotationSpeed;
        _slider.onValueChanged.AddListener(delegate { Change(); });
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(delegate { Change(); });
    }

    private void Change()
    {
        _firstPersonController.RotationSpeed = _slider.value;
        _playerSettingSaving.Set(_slider.value);
    }
}
