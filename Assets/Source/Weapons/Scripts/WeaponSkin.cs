using UnityEngine;

public class WeaponSkin : MonoBehaviour
{
    private const string Skin = "Skin";
    private const string Skin1 = "Skin1";
    private const string Skin2 = "Skin2";
    private const string Skin3 = "Skin3";
    private const string Skin4 = "Skin4";
    private const string Skin5 = "Skin5";
    private const string Skin6 = "Skin6";
    private const string Skin7 = "Skin7";
    private const string Skin8 = "Skin8";

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

    private WeaponInput _getting;
    private Weapon _weapon;
    private WeaponSkinSaving _weaponSkinSaving;

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
        Skin8
    }

    private void Awake()
    {
        _getting = GetComponentInParent<WeaponInput>();
        _weapon = GetComponent<Weapon>();
        _weaponSkinSaving = _getting.WeaponSkinSaving;

        string name = _weaponSkinSaving.GetCurrent(_weapon.Type);

        switch (name)
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
            default:
                _skinnedMeshRenderer.material = _defaultSkin;
                break;
        }
    }
}
