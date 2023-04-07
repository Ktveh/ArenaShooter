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
        _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(Authorization);
        if (PlayerAccount.IsAuthorized)
            _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(TopPlayers);

    }
}
