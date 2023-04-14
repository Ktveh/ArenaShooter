using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;
using DungeonGames.VKGames;

public class Language : MonoBehaviour
{
    [SerializeField] private int _indexOfFirstScene;

    private const float WaitTime = 0.25f;
    private const string EnglishCode = "en";
    private const string RussianCode = "ru";
    private const string TurkishCode = "tr";
    private const string EnglishLanguage = "English";
    private const string RussianLanguage = "Russian";
    private const string TurkishLanguage = "Turkish";

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR || VK_GAMES
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(RussianLanguage);
        SceneManager.LoadScene(_indexOfFirstScene);
        yield break;
#endif

#if VK_GAMES
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(RussianLanguage);
        SceneManager.LoadScene(_indexOfFirstScene);
        yield return VKGamesSdk.Initialize();
        yield break;
#endif

#if YANDEX_GAMES
        yield return YandexGamesSdk.Initialize();

        if (YandexGamesSdk.IsInitialized)
        {
            ChangeLanguage();
        }

        while (!YandexGamesSdk.IsInitialized)
        {
            yield return new WaitForSeconds(WaitTime);

            if (YandexGamesSdk.IsInitialized)
            {
                ChangeLanguage();
            }
        }
#endif
    }

    private void ChangeLanguage()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;
        string language;
        switch (languageCode)
        {
            case EnglishCode:
                language = EnglishLanguage;
                break;
            case RussianCode:
                language = RussianLanguage;
                break;
            case TurkishCode:
                language = TurkishLanguage;
                break;
            default:
                language = EnglishLanguage;
                break;
        }

        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(language);

        SceneManager.LoadScene(_indexOfFirstScene);
    }
}