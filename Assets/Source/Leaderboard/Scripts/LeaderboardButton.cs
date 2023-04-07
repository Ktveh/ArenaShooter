using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using TMPro;

public class LeaderboardButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;

    private const string Authorization = "Authorization";
    private const string TopPlayers = "The Best Players";

    private void Awake()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        Debug.Log(_buttonText.text);
        _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(TopPlayers);
        Debug.Log(Lean.Localization.LeanLocalization.GetTranslationText(TopPlayers));
        Debug.Log(_buttonText.text);
        return;
#endif

        if (PlayerAccount.IsAuthorized)
            _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(TopPlayers);
        else
            _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(Authorization);
    }
}
