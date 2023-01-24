using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private float _delay;

    private MenuShowingScore _menuShowingScore;
    private MenuUpgradingWeapon _menuUpgradingWeapon;

    public event UnityAction Showed;
 
    private void Awake()
    {
        _menuShowingScore = GetComponentInChildren<MenuShowingScore>();
        _menuUpgradingWeapon = GetComponentInChildren<MenuUpgradingWeapon>();

        _menuShowingScore.gameObject.SetActive(false);
        _menuUpgradingWeapon.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke(nameof(ShowScore), _delay);
    }

    private void ShowScore()
    {
        _menuShowingScore.SetValue();
        _background.SetActive(true);
        _menuShowingScore.gameObject.SetActive(true);
        Showed?.Invoke();
    }
}
