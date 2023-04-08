using UnityEngine;
using UnityEngine.Events;

public class ShowingScore : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private float _delay;
    [SerializeField] private UnityEvent _showed;

    private GettingScore _menuShowingScore;

    public event UnityAction Showed;
 
    private void Awake()
    {
        _menuShowingScore = GetComponentInChildren<GettingScore>();

        _menuShowingScore.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _background.SetActive(true);
        Invoke(nameof(ShowScore), _delay);
    }

    private void ShowScore()
    {
        _menuShowingScore.SetValue();
        _menuShowingScore.gameObject.SetActive(true);
        _showed?.Invoke();
        Showed?.Invoke();
    }
}
