using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponSkin : MonoBehaviour
{
    private const float SmallSize = 600f;
    private const float MediumSize = 190f;
    private const float BigSize = 50f;
    private const string Skin = "Skin";
    private const string Skin1 = "Skin1";
    private const string Skin2 = "Skin2";
    private const string Skin3 = "Skin3";
    private const string Skin4 = "Skin4";
    private const string Skin5 = "Skin5";
    private const string Skin6 = "Skin6";
    private const string Skin7 = "Skin7";
    private const string Skin8 = "Skin8";
    private const string Skin9 = "Skin9";
    private const string Skin10 = "Skin10";
    private const string Skin11 = "Skin11";
    private const string Skin12 = "Skin12";
    private const string Skin13 = "Skin13";

    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Material _defaultSkin;
    [SerializeField] private Material _skin;
    [SerializeField] private Material _skin1;
    [SerializeField] private Material _skin2;
    [SerializeField] private Material _skin3;
    [SerializeField] private Material _skin4;
    [SerializeField] private Material _skin5;
    [SerializeField] private Material _skin6;
    [SerializeField] private Material _skin7;
    [SerializeField] private Material _skin8;
    [SerializeField] private Material _skin9;
    [SerializeField] private Material _skin10;
    [SerializeField] private Material _skin11;
    [SerializeField] private Material _skin12;
    [SerializeField] private Material _skin13;

    private WeaponInput _getting;
    private Weapon _weapon;
    private WeaponSkinSaving _weaponSkinSaving;
    private PlayerWeaponSelecting _playerWeaponSelecting;

    private string _nameSkin => _weaponSkinSaving.GetCurrent(_weapon.Type);

    public enum Names
    {
        Default,
        Skin,
        Skin1,
        Skin2,
        Skin3,
        Skin4,
        Skin5,
        Skin6,
        Skin7,
        Skin8,
        Skin9,
        Skin10,
        Skin11,
        Skin12,
        Skin13
    }

    private void Awake()
    {
        _getting = GetComponentInParent<WeaponInput>();
        _weapon = GetComponent<Weapon>();
        _weaponSkinSaving = _getting.WeaponSkinSaving;
        _playerWeaponSelecting = _getting.PlayerWeaponSelecting;

        switch (_nameSkin)
        {
            case Skin:
                _skinnedMeshRenderer.material = _skin;
                break;
            case Skin1:
                _skinnedMeshRenderer.material = _skin1;
                break;
            case Skin2:
                _skinnedMeshRenderer.material = _skin2;
                break;
            case Skin3:
                _skinnedMeshRenderer.material = _skin3;
                break;
            case Skin4:
                _skinnedMeshRenderer.material = _skin4;
                break;
            case Skin5:
                _skinnedMeshRenderer.material = _skin5;
                break;
            case Skin6:
                _skinnedMeshRenderer.material = _skin6;
                break;
            case Skin7:
                _skinnedMeshRenderer.material = _skin7;
                break;
            case Skin8:
                _skinnedMeshRenderer.material = _skin8;
                break;
            case Skin9:
                _skinnedMeshRenderer.material = _skin9;
                break;
            case Skin10:
                _skinnedMeshRenderer.material = _skin10;
                break;
            case Skin11:
                _skinnedMeshRenderer.material = _skin11;
                break;
            case Skin12:
                _skinnedMeshRenderer.material = _skin12;
                break;
            case Skin13:
                _skinnedMeshRenderer.material = _skin13;
                break;
            default:
                _skinnedMeshRenderer.material = _defaultSkin;
                break;
        }
    }

    private void OnEnable()
    {
        _playerWeaponSelecting.Selected += OnSelcted;
    }

    private void OnDisable()
    {
        _playerWeaponSelecting.Selected -= OnSelcted;
    }

    private void OnSelcted()
    {
        if (_nameSkin != "")
        {
            if (_nameSkin != Names.Default.ToString())
            {
                switch (_playerWeaponSelecting.CurrentWeapon.Type)
                {
                    case Weapon.Types.Rifle:
                        _skinnedMeshRenderer.material.mainTextureScale = new Vector2(BigSize, BigSize);
                        break;
                    case Weapon.Types.SniperRifle:
                        _skinnedMeshRenderer.material.mainTextureScale = new Vector2(SmallSize, SmallSize);
                        break;
                    default:
                        _skinnedMeshRenderer.material.mainTextureScale = new Vector2(MediumSize, MediumSize);
                        break;
                }
            }
        }
    }
}
