using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    [SerializeField] private float _delay;
    [SerializeField] private bool _isMainMenu;

    private MenuShowingScore _menuShowingScore;
    private MenuUpgradingWeapon _menuUpgradingWeapon;

    public event UnityAction Showed;
 
    private void Awake()
    {
        _menuShowingScore = GetComponentInChildren<MenuShowingScore>();
        _menuUpgradingWeapon = GetComponentInChildren<MenuUpgradingWeapon>();

        if (_isMainMenu == false)
            _menuUpgradingWeapon.gameObject.SetActive(false);

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
        Showed?.Invoke();
    }
}
