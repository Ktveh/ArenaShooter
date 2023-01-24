using TMPro;
using UnityEngine;

public class ShowingPlayerWallet : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private TMP_Text _value;

    private void OnEnable()
    {
        _playerWallet.ChangedValue += OnChangedValue;

        _value.text = _playerWallet.Value.ToString();
    }

    private void OnDisable()
    {
        _playerWallet.ChangedValue -= OnChangedValue;
    }

    private void OnChangedValue(int value)
    {
        _value.text = value.ToString();
    }
}
