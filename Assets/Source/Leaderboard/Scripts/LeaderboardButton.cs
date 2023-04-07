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
        _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(TopPlayers);
        return;
#endif

        if (PlayerAccount.IsAuthorized)
            _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(TopPlayers);
        else
            _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(Authorization);
    }
}
