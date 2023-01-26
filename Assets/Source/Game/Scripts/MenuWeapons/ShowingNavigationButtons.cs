using UnityEngine;
using UnityEngine.UI;

public class ShowingNavigationButtons : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Button _nextLevel;
    [SerializeField] private Button _currentLevel;
    [SerializeField] private Button _mainMenu;

    private void OnEnable()
    {
        _currentLevel.gameObject.SetActive(_game.PlayerIsDead);
        _mainMenu.gameObject.SetActive(_game.PlayerIsDead);
        _nextLevel.gameObject.SetActive(_game.PlayerIsDead ? false : true);
    }
}
