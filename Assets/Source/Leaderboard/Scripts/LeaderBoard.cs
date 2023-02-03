using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private GameObject _content;
    [SerializeField] private LeaderView _template;

    private const string LeaderBoardName = "LeaderBoard";
    private const string LeaderBoardTitle = "Zombies are killed";
    private const string Anonymous = "Anonymous";
    private const int MaxResult = 15;

    private void OnEnable()
    {
        ShowLeaders();
        _name.text = Lean.Localization.LeanLocalization.GetTranslationText(LeaderBoardTitle);
    }

    private void OnDisable()
    {
        foreach (Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowLeaders()
    {
        if (!PlayerAccount.IsAuthorized)
        {
            PlayerAccount.Authorize();
            gameObject.SetActive(false);
            return;
        }

        Leaderboard.GetEntries(LeaderBoardName, (result) =>
        {
            foreach (var entry in result.entries)
            {
                string name = entry.player.publicName;
                if (string.IsNullOrEmpty(name))
                {
                    name = Lean.Localization.LeanLocalization.GetTranslationText(Anonymous);
                }
                int score = entry.score;
                int place = entry.rank;
                if (place > MaxResult)
                {
                    break;
                }
                AddLeader(name, score, place);
            }
        });
    }

    private void AddLeader(string name, int score, int place)
    {
        var view = Instantiate(_template, _content.transform);
        view.Render(name, score.ToString(), place.ToString());
    }
}
