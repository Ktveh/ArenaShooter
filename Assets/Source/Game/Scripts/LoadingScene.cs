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
        if (_coroutine == null)
            _coroutine = StartCoroutine(WaitForSaving(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(WaitForSaving(_zombieCounter.NextLevel));
    }

    public void LoadMainMenu()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(WaitForSaving(0));
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
