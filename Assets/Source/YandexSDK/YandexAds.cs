using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class YandexAds : MonoBehaviour
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
        Close();
        return;
#endif
        VideoAd.Show(Opene, GetGold, Close, Error);
    }

    private void OnDownUpgrade(WeaponAccessories.Type type)
    {
        _type = type;
#if !UNITY_WEBGL || UNITY_EDITOR
        Upgrade();
        Close();
        return;
#endif
        VideoAd.Show(Opene, Upgrade, Close, Error);
    }

    private void OnLevelCompleted()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        Close();
        return;
#endif
        InterstitialAd.Show(Opene, Close, Error);
    }

    private void Opene()
    {
        Shows?.Invoke();
    }

    private void GetGold()
    {
        GetedGold?.Invoke();
    }

    private void Upgrade()
    {
        Upgraded?.Invoke(_type);
    }

    private void Close()
    {
        Showed?.Invoke();
    }

    private void Close(bool onClose)
    {
        Showed?.Invoke();
    }

    private void Error(string onError)
    {
        Errored?.Invoke();
    }
}