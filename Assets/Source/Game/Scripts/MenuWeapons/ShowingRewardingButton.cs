using UnityEngine;

public class ShowingRewardingButton : MonoBehaviour
{
    private const int minPercentage = 0;
    private const int maxPercentage = 100;

    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;
    [SerializeField] private ButtonSelectingAccessory[] _buttonsBuy;
    [SerializeField] private ButtonUpgradingOnReward[] _buttonsReward;
    [Range(0,100)]
    [SerializeField] int _probability;

    private void OnEnable()
    {
        _playerWeaponSelecting.Selected += OnSelected;
    }

    private void OnDisable()
    {
        _playerWeaponSelecting.Selected -= OnSelected;
    }

    private void OnSelected()
    {
        foreach (var buttonBuy in _buttonsBuy)
            buttonBuy.gameObject.SetActive(true);

        foreach (var buttonReward in _buttonsReward)
            buttonReward.gameObject.SetActive(false);

        if (_buttonsBuy.Length != _buttonsReward.Length)
            return;

        if (Random.Range(minPercentage, maxPercentage) <= _probability)
        {
            int number = Random.Range(0, _buttonsReward.Length);
            _buttonsBuy[number].gameObject.SetActive(false);
            _buttonsReward[number].gameObject.SetActive(true);
        }
    }
}
