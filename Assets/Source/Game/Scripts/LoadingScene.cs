using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private ZombieCounter _zombieCounter;

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(_zombieCounter.NextLevel);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
