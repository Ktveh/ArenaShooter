using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class YandexAds : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private SoundButton _soundButton;
    [SerializeField] private ButtonUpgradingOnReward[] _buttonsUpgradingOnReward;

    private WeaponAccessories.Type _type;

    public event UnityAction Shows;
    public event UnityAction Showed;
    public event UnityAction Errored;
    public event UnityAction<WeaponAccessories.Type> Rewarded;

    private void OnEnable()
    {
        foreach (var button in _buttonsUpgradingOnReward)
            button.Down += OnDown;

        _game.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        foreach (var button in _buttonsUpgradingOnReward)
            button.Down -= OnDown;

        _game.LevelCompleted += OnLevelCompleted;
    }

    private void OnDown(WeaponAccessories.Type type)
    {
        _type = type;
        VideoAd.Show(Opene, Reward, Close, Error);
    }

    private void OnLevelCompleted()
    {
        InterstitialAd.Show(Opene, Close, Error);
    }

    private void Opene()
    {
        _soundButton.OnAdShow();
        Shows?.Invoke();
    }

    private void Reward()
    {
        Rewarded?.Invoke(_type);
    }

    private void Close()
    {
        _soundButton.OnAdClose();
        Showed?.Invoke();
    }

    private void Close(bool onClose)
    {
        _soundButton.OnAdClose();
        Showed?.Invoke();
    }

    private void Error(string onError)
    {
        _soundButton.OnAdClose();
        Errored?.Invoke();
    }
}