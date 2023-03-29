using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float Step = 1f;

    [SerializeField] private RectTransform _target;
    [SerializeField] private Button _button;
    [SerializeField] private float _addition = 10f;

    private Vector2 _defaulSize;
    private Vector2 _activeSize;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _defaulSize = _target.sizeDelta;
        _activeSize = _target.sizeDelta + new Vector2(_addition, _addition);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button != null)
        {
            if (_button.interactable == false)
                return;
        }

        if(_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(Change(_activeSize));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_button != null)
        {
            if (_button.interactable == false)
                return;
        }

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(Change(_defaulSize));
    }

    private IEnumerator Change(Vector2 target)
    {
        if (target == _defaulSize)
        {
            while (_target.sizeDelta.x >= _defaulSize.x)
            {
                _target.sizeDelta -= new Vector2(Step, Step);
                yield return null;
            }
        }
        else if(target == _activeSize)
        {
            while (_target.sizeDelta.x <= _activeSize.x)
            {
                _target.sizeDelta += new Vector2(Step, Step);
                yield return null;
            }
        }
    }
}
