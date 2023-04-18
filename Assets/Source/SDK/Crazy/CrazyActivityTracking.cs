using UnityEngine;
using CrazyGames;
using UnityEngine.SceneManagement;

public class CrazyActivityTracking : MonoBehaviour
{
    private const int NumberLoadingScene = 0;
    private const int NumberTitleScene = 1;

    [SerializeField] private PausingGame _pausingGame;
    [SerializeField] private Game _game;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != NumberLoadingScene)
        {
            if (SceneManager.GetActiveScene().buildIndex != NumberTitleScene)
                OnPlayed();
            else
                OnPaused();
        }
        else
        {
            OnPaused();
        }
    }

    private void OnEnable()
    {
        _pausingGame.Played += OnPlayed;
        _pausingGame.Paused += OnPaused;
        _game.LevelCompleted += OnPaused;
    }

    private void OnDisable()
    {
        _pausingGame.Played -= OnPlayed;
        _pausingGame.Paused -= OnPaused;
        _game.LevelCompleted -= OnPaused;
    }

    private void OnPlayed()
    {
        CrazyEvents.Instance.GameplayStart();
    }

    private void OnPaused()
    {
        CrazyEvents.Instance.GameplayStop();
    }
}
