using UnityEngine;

[RequireComponent(typeof(YandexInitialization))]
[RequireComponent(typeof(VKInitialization))]
[RequireComponent(typeof(YandexAds))]
[RequireComponent(typeof(VKAds))]
public class EnablingSDK : MonoBehaviour
{
    private YandexInitialization _yandexInitialization;
    private VKInitialization _vkInitialization;
    private YandexAds _yandexAds;
    private VKAds _vkAds;

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
}
