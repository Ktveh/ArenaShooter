using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponAccessories : MonoBehaviour
{
    [SerializeField] private Sprite _iconScope1;
    [SerializeField] private Sprite _iconScope2;
    [SerializeField] private Sprite _iconSilencer;
    [SerializeField] private int _priceScope1;
    [SerializeField] private int _priceScope2;
    [SerializeField] private int _priceSilencer;
    [SerializeField] private Sprite _scope1Texture;
    [SerializeField] private float _scope1TextureSize = 0.01f;
    [SerializeField] private Sprite _scope2Texture;
    [SerializeField] private float _scope2TextureSize = 0.025f;
    [SerializeField] private SkinnedMeshRenderer _scopeRenderer;
    [SerializeField] private SkinnedMeshRenderer _scope1Renderer;
    [SerializeField] private SkinnedMeshRenderer _scope2Renderer;
    [SerializeField] private SkinnedMeshRenderer _silencerRenderer;
    [SerializeField] private GameObject _scope1RenderMesh;
    [SerializeField] private GameObject _scope2RenderMesh;
    [SerializeField] private SpriteRenderer _scope1SpriteRenderer;
    [SerializeField] private SpriteRenderer _scope2SpriteRenderer;

    private WeaponInput _getting;
    private Weapon _weapon;
    private WeaponSaving _weaponSaving;
    private WeaponAccessoriesSaving _weaponAccessoriesSaving;

    public Sprite IconScope1 => _iconScope1;
    public Sprite IconScope2 => _iconScope2;
    public Sprite IconSilencer => _iconSilencer;
    public int PriceScope1 => _weaponAccessoriesSaving.Check(_weapon.Type, Type.Scope1) ? 0 : _priceScope1;
    public int PriceScope2 => _weaponAccessoriesSaving.Check(_weapon.Type, Type.Scope2) ? 0 : _priceScope2;
    public int PriceSilencer => _weaponAccessoriesSaving.Check(_weapon.Type, Type.Silencer) ? 0 : _priceSilencer;
    public bool IsEnabledScope => _scopeRenderer != null && IsEnabledScope1 == false && IsEnabledScope2 == false;
    public bool IsEnabledScope1 => _scope1Renderer != null && _weaponSaving.TryGetAccessory(_weapon.Type, Type.Scope1);
    public bool IsEnabledScope2 => _scope2Renderer != null && _weaponSaving.TryGetAccessory(_weapon.Type, Type.Scope2);
    public bool IsEnabledSilencer => _silencerRenderer != null && _weaponSaving.TryGetAccessory(_weapon.Type, Type.Silencer);

    public enum Type
    {
        Scope,
        Scope1,
        Scope2,
        Silencer
    }

    private void Awake()
    {
        _getting = GetComponentInParent<WeaponInput>();
        _weaponSaving = _getting.WeaponSaving;
        _weaponAccessoriesSaving = _getting.WeaponAccessoriesSaving;
        _weapon = GetComponent<StandardWeapon>();
    }

    private void Start()
    {
        if (IsEnabledScope1)
        {
            if (IsEnabledScope1)
            {
                _scope1Renderer.enabled = true;
                _scope1RenderMesh.SetActive(true);
                _scope1SpriteRenderer.sprite = _scope1Texture;
                _scope1SpriteRenderer.transform.localScale = new Vector3(_scope1TextureSize, _scope1TextureSize, _scope1TextureSize);
            }
            else
            {
                _scope1Renderer.enabled = false;
                _scope1RenderMesh.SetActive(false);
            }
        }
        else if (IsEnabledScope2)
        {
            if (IsEnabledScope2)
            {
                _scope2Renderer.enabled = true;
                _scope2RenderMesh.SetActive(true);
                _scope2SpriteRenderer.sprite = _scope2Texture;
                _scope2SpriteRenderer.transform.localScale = new Vector3(_scope2TextureSize, _scope2TextureSize, _scope2TextureSize);
            }
            else
            {
                _scope2Renderer.enabled = false;
                _scope2RenderMesh.SetActive(false);
            }
        }
        else
        {
            _scopeRenderer.enabled = true;
        }


        if ((IsEnabledSilencer))
            _silencerRenderer.enabled = true;
        else
            _silencerRenderer.enabled = false;
    }
}
