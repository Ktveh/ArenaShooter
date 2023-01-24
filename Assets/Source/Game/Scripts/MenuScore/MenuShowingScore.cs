using TMPro;
using UnityEngine;

public class MenuShowingScore : MonoBehaviour
{
    [SerializeField] private LevelScore _levelScore;
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private TMP_Text _amountKilledZombie;
    [SerializeField] private TMP_Text _amountHitedHead;
    [SerializeField] private TMP_Text _amountShot;
    [SerializeField] private TMP_Text _hitAccuracy;
    [SerializeField] private TMP_Text _rewardingAmountKilledZombie;
    [SerializeField] private TMP_Text _rewardingAmountHitedHead;
    [SerializeField] private TMP_Text _rewardingHitAccuracy;
    [SerializeField] private TMP_Text _allReward;

    public void SetValue()
    {
        _amountKilledZombie.text = _levelScore.AmountKilledZombie.ToString();
        _amountHitedHead.text = _levelScore.AmountHitedHead.ToString();
        _hitAccuracy.text = (int)_levelScore.HitAccuracy + "%";
        _rewardingAmountKilledZombie.text = "+ " + _levelReward.KilledZombie.ToString();
        _rewardingAmountHitedHead.text = "+ " + _levelReward.HitedHead.ToString();
        _rewardingHitAccuracy.text = "+ " + _levelReward.HitAccuracy.ToString();
        _amountShot.text = _levelScore.AmountShot.ToString();
        _allReward.text = "+ " + _levelReward.AllReward.ToString();
    }
}
