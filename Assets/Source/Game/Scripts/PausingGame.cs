using UnityEngine;

public class PausingGame : MonoBehaviour
{
    [SerializeField] private YandexAds _yandexAds;

    private void OnEnable()
    {
        _yandexAds.Shows += OnShows;
        _yandexAds.Showed += OnShowed;
        _yandexAds.Errored += OnErrored;
    }

    private void OnDisable()
    {
        _yandexAds.Shows -= OnShows;
        _yandexAds.Showed -= OnShowed;
        _yandexAds.Errored -= OnErrored;
    }

    private void OnShows()
    {
        Time.timeScale = 0;
    }
    
    private void OnShowed()
    {
        Time.timeScale = 1;
    }
    
    private void OnErrored()
    {
        Time.timeScale = 1;
    }
}
