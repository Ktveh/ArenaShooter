using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingCameraSensitivity : MonoBehaviour
{
    [SerializeField] private PlayerDirection _playerDirection;

    private Slider _slider;

    public event UnityAction<float> Changed;

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
    }
}
