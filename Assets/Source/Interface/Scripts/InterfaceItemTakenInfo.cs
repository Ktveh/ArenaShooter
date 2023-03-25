using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceItemTakenInfo : MonoBehaviour
{
    private const float Delay = 1f;

    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GameObject _itemTakenInfo;
    [SerializeField] private TMP_Text _amount;
    [SerializeField] private Image _SMG;
    [SerializeField] private Image _rifle;
    [SerializeField] private Image _shotgun;
    [SerializeField] private Image _sniperRifle;
    [SerializeField] private Image _drug;
    [SerializeField] private Image _grenadeLauncher;
    [SerializeField] private Image _grenade;

    private void OnEnable()
    {
        _playerInventory.Taked += OnTaked;
        _playerHealth.Taked += OnTaked;
    }

    private void OnDisable()
    {
        _playerInventory.Taked -= OnTaked;
        _playerHealth.Taked -= OnTaked;
    }

    private void OnTaked(Item item, uint amount)
    {
        _SMG.gameObject.SetActive(false);
        _rifle.gameObject.SetActive(false);
        _shotgun.gameObject.SetActive(false);
        _sniperRifle.gameObject.SetActive(false);
        _drug.gameObject.SetActive(false);
        _grenadeLauncher.gameObject.SetActive(false);
        _grenade.gameObject.SetActive(false);

        _amount.text = "+ " + amount;

        switch (item.Type)
        {
            case Item.Types.SMG:
                _SMG.gameObject.SetActive(true);
                break;
            case Item.Types.Rifle:
                _rifle.gameObject.SetActive(true);
                break;
            case Item.Types.Shotgun:
                _shotgun.gameObject.SetActive(true);
                break;
            case Item.Types.SniperRifle:
                _sniperRifle.gameObject.SetActive(true);
                break;
            case Item.Types.Drug:
                _drug.gameObject.SetActive(true);
                break;
            case Item.Types.GrenadeLauncher:
                _grenadeLauncher.gameObject.SetActive(true);
                break;
            case Item.Types.Grenade:
                _grenade.gameObject.SetActive(true);
                break;
        }

        _itemTakenInfo.gameObject.SetActive(true);
        Invoke(nameof(Hide), Delay);
    }

    private void Hide()
    {
        _itemTakenInfo.gameObject.SetActive(false);
    }
}
