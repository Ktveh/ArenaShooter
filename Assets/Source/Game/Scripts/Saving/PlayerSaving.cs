using UnityEngine;

public class PlayerSaving : MonoBehaviour
{
    private const string Money = "Money";

    [SerializeField] private PlayerWallet _playerWallet;

    private void Awake()
    {
        _playerWallet.SetValue(PlayerPrefs.GetInt(Money));
    }

    private void OnEnable()
    {
        _playerWallet.ChangedValue += OnChangedValue;
    }

    private void OnDisable()
    {
        _playerWallet.ChangedValue -= OnChangedValue;
    }

    private void OnChangedValue(int value)
    {
        PlayerPrefs.SetInt(Money, value);
    }
}
