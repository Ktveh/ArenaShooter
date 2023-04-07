using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using TMPro;

public class LeaderboardButton : MonoBehaviour
{
    [SerializeField] private LeaderBoard _leaderboard;
    [SerializeField] private TextMeshProUGUI _buttonText;

    private const string Authorization = "Authorization";
    private const string TopPlayers = "The Best Players";                                     

    private void Awake()
    {
        _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(Authorization);
        if (PlayerAccount.IsAuthorized)
            Rename();
    }

    public void OnClick()
    {
        if (!PlayerAccount.IsAuthorized)
        {
            PlayerAccount.Authorize(Rename);
        }
        else
        {
            _leaderboard.gameObject.SetActive(true);
        }
    }

    private void Rename()
    {
        _buttonText.text = Lean.Localization.LeanLocalization.GetTranslationText(TopPlayers);
    }
}
