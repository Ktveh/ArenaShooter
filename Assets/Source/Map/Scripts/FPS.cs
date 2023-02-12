using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;

    private int _value;
    private float _ellapsedTime;

    private void Update()
    {
        _ellapsedTime += Time.deltaTime;
        _value++;
        if (_ellapsedTime >= 1)
        {
            _label.text = string.Format($"{_value} FPS");
            _value = 0;
            _ellapsedTime = 0;
        }
    }
}
