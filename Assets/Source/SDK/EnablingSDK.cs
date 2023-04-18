using UnityEngine;

[RequireComponent(typeof(YandexInitialization))]
[RequireComponent(typeof(YandexAds))]
[RequireComponent(typeof(VKInitialization))]
[RequireComponent(typeof(VKAds))]
[RequireComponent(typeof(CrazyInitialization))]
[RequireComponent(typeof(CrazyAdvertising))]
[RequireComponent(typeof(CrazyActivityTracking))]
public class EnablingSDK : MonoBehaviour
{
    private YandexInitialization _yandexInitialization;
    private YandexAds _yandexAds;
    private VKInitialization _vkInitialization;
    private VKAds _vkAds;
    private CrazyInitialization _crazyInitialization;
    private CrazyAdvertising _crazyAdvertising;
    private CrazyActivityTracking _crazyActivityTracking;

#if YANDEX_GAMES
    private void Start()
    {
        _yandexInitialization = GetComponent<YandexInitialization>();
        _yandexAds = GetComponent<YandexAds>();

        _yandexInitialization.enabled = true;
        _yandexAds.enabled = true;
    }
#endif

#if VK_GAMES
    private void Start()
    {
        _vkInitialization = GetComponent<VKInitialization>();
        _vkAds = GetComponent<VKAds>();

        _vkInitialization.enabled = true;
        _vkAds.enabled = true;
    }
#endif

#if CRAZY_GAMES
    private void Start()
    {
        _crazyInitialization = GetComponent<CrazyInitialization>();
        _crazyAdvertising = GetComponent<CrazyAdvertising>();
        _crazyActivityTracking = GetComponent<CrazyActivityTracking>();

        _crazyInitialization.enabled = true;
        _crazyAdvertising.enabled = true;
        _crazyActivityTracking.enabled = true;
    }
#endif
}
