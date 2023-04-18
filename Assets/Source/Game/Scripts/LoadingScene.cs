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
#if !YANDEX_GAMES
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#else
        if (_coroutine == null)
            _coroutine = StartCoroutine(WaitForSaving(SceneManager.GetActiveScene().buildIndex));
#endif
    }

    public void LoadNextLevel()
    {
#if !YANDEX_GAMES
        SceneManager.LoadScene(_zombieCounter.NextLevel);
#else
        if (_coroutine == null)
            _coroutine = StartCoroutine(WaitForSaving(_zombieCounter.NextLevel));
#endif
    }

    public void LoadMainMenu()
    {
#if !YANDEX_GAMES
        SceneManager.LoadScene(0);
#else
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
