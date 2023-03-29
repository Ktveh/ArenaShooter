using UnityEngine;
using UnityEngine.UI;

public class ChangingColorImage : MonoBehaviour
{
    [SerializeField] private Image _images;
    [SerializeField] private Color _targetColor;

    private Color _defaultColor;

    private void Awake()
    {
        _defaultColor = _images.color;
    }

    public void ChangeToTarget()
    {
        _images.color = _targetColor;
    }
    
    public void ChangeToDefault()
    {
        _images.color = _defaultColor;
    }
}
