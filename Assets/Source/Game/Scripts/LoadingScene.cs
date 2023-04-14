using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private ZombieCounter _zombieCounter;
    [SerializeField] private SavingToCloud _savingToCloud;

    private Coroutine _coroutine;

    public void LoadCurrentLevel()
    {
#if VK_GAMES
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#endif

#if YANDEX_GAMES
        if (_coroutine == null)
            _coroutine = StartCoroutine(WaitForSaving(SceneManager.GetActiveScene().buildIndex));
#endif
    }

    public void LoadNextLevel()
    {
#if VK_GAMES
        SceneManager.LoadScene(_zombieCounter.NextLevel);
#endif

#if YANDEX_GAMES
        if (_coroutine == null)
            _coroutine = StartCoroutine(WaitForSaving(_zombieCounter.NextLevel));
#endif
    }

    public void LoadMainMenu()
    {
#if VK_GAMES
        SceneManager.LoadScene(0);
#endif

#if YANDEX_GAMES
        if (_coroutine == null)
            _coroutine = StartCoroutine(WaitForSaving(0));
#endif
    }

    private IEnumerator WaitForSaving(int buildIndex)
    {
        _savingToCloud.enabled = true;

        while (_savingToCloud.IsSuccess == false)
        {
            yield return null;
        }

        SceneManager.LoadScene(buildIndex);
    }
}
