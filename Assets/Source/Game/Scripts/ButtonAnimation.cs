using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float Step = 1f;

    [SerializeField] private RectTransform _button;
    [SerializeField] private float Addition = 10f;

    private Vector2 _defaulSize;
    private Vector2 _activeSize;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _defaulSize = _button.sizeDelta;
        _activeSize = _button.sizeDelta + new Vector2(Addition, Addition);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(Change(_activeSize));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(Change(_defaulSize));
    }

    private IEnumerator Change(Vector2 target)
    {
        if (target == _defaulSize)
        {
            while (_button.sizeDelta.x >= _defaulSize.x)
            {
                _button.sizeDelta -= new Vector2(Step, Step);
                yield return null;
            }
        }
        else if(target == _activeSize)
        {
            while (_button.sizeDelta.x <= _activeSize.x)
            {
                _button.sizeDelta += new Vector2(Step, Step);
                yield return null;
            }
        }
    }
}
