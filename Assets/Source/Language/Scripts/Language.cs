using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

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
#if !UNITY_WEBGL || UNITY_EDITOR
        Lean.Localization.LeanLocalization.SetCurrentLanguageAll(RussianLanguage);
        SceneManager.LoadScene(_indexOfFirstScene);
        yield break;
#endif

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