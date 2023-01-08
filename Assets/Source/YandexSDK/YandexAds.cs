using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class YandexAds : MonoBehaviour
{
    public bool IsShows { get; private set; }

    public event UnityAction Shows;
    public event UnityAction Showed;
    public event UnityAction Errored;
    public event UnityAction Rewarded;

    private void ShowRewarded()
    {
        VideoAd.Show(Opene, Reward, Close, Error);
    }

    private void ShowInterstitial()
    {
        InterstitialAd.Show(Opene, Close, Error);
    }

    private void Opene()
    {
        IsShows = true;
        Shows?.Invoke();
    }

    private void Reward()
    {
        Rewarded?.Invoke();
    }

    private void Close()
    {
        IsShows = false;
        Showed?.Invoke();
    }

    private void Close(bool onClose)
    {
        IsShows = false;
        Showed?.Invoke();
    }

    private void Error(string onError)
    {
        IsShows = false;
        Errored?.Invoke();
    }
}