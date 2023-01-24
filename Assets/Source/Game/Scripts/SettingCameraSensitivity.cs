using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SettingCameraSensitivity : MonoBehaviour
{
    [SerializeField] private PlayerDirection _playerDirection;
    [SerializeField] private PlayerSettingSaving _playerSettingSaving;

    private Slider _slider;

    public event UnityAction<float> Changed;

    private void Awake()
    {
        Changed?.Invoke(_playerSettingSaving.CurrentCameraSensitivity);
    }

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _slider.value = _playerDirection.Sensitivity;
        _slider.onValueChanged.AddListener(delegate { Change(); });
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(delegate { Change(); });
    }

    private void Change()
    {
        Changed?.Invoke(_slider.value);
        _playerSettingSaving.Set(_slider.value);
    }
}
