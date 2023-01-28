using UnityEngine;
using TMPro;

public class LeaderView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _rank;

    public void Render(string name, string score, string rank)
    {
        _name.text = name;
        _score.text = score;
        _rank.text = rank;
    }
}
