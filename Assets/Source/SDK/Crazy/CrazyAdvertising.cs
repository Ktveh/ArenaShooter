using UnityEngine;
using CrazyGames;
using UnityEngine.Events;

public class CrazyAdvertising : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private AdRewardButton _adRewardButton;
    [SerializeField] private ButtonUpgradingOnReward[] _buttonsUpgradingOnReward;

    private WeaponAccessories.Type _type;

    public event UnityAction Shows;
    public event UnityAction Showed;
    public event UnityAction Errored;
    public event UnityAction GetedGold;
    public event UnityAction<WeaponAccessories.Type> Upgraded;

    private void OnEnable()
    {
        foreach (var button in _buttonsUpgradingOnReward)
            button.Down += OnDownUpgrade;

        _adRewardButton.Down += OnDownReward;
        _game.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        foreach (var button in _buttonsUpgradingOnReward)
            button.Down -= OnDownUpgrade;

        _adRewardButton.Down -= OnDownReward;
        _game.LevelCompleted -= OnLevelCompleted;
    }

    private void OnDownReward()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        GetGold();
        return;
#endif

        CrazyAds.Instance.beginAdBreakRewarded(GetGold, Error);
    }

    private void OnDownUpgrade(WeaponAccessories.Type type)
    {
        _type = type;
#if !UNITY_WEBGL || UNITY_EDITOR
        Upgrade();
        return;
#endif

        CrazyAds.Instance.beginAdBreakRewarded(Upgrade, Error);
    }

    private void OnLevelCompleted()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        Close();
        return;
#endif

        CrazyAds.Instance.beginAdBreak(Opene, Error);
    }

    private void Opene()
    {
        Shows?.Invoke();
        Close();
    }

    private void GetGold()
    {
        GetedGold?.Invoke();
        Close();
    }

    private void Upgrade()
    {
        Upgraded?.Invoke(_type);
        Close();
    }

    private void Close()
    {
        Showed?.Invoke();
    }

    private void Error()
    {
        Errored?.Invoke();
    }
}
