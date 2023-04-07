using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using TMPro;

public class LeaderboardButton : MonoBehaviour
{
    [SerializeField] private LeaderBoard _leaderboard;
    [SerializeField] private TextMeshProUGUI _buttonAuthText;                                  
    [SerializeField] private TextMeshProUGUI _buttonTopText;                                  

    private void Awake()
    {
        _buttonAuthText.gameObject.SetActive(true);
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
        _buttonAuthText.gameObject.SetActive(false);
        _buttonTopText.gameObject.SetActive(true);
    }
}
