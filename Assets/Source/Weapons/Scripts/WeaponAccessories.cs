using UnityEngine;

public class WeaponAccessories : MonoBehaviour
{
    [SerializeField] private Sprite _scope1Texture;
    [SerializeField] private float _scope1TextureSize = 0.01f;
    [SerializeField] private Sprite _scope2Texture;
    [SerializeField] private float _scope2TextureSize = 0.025f;
    [SerializeField] private bool _isEnabledScope;
    [SerializeField] private bool _isEnabledScope1;
    [SerializeField] private bool _isEnabledScope2;
    [SerializeField] private bool _isEnabledSilencer;
    [SerializeField] private SkinnedMeshRenderer _scopeRenderer;
    [SerializeField] private SkinnedMeshRenderer _scope1Renderer;
    [SerializeField] private SkinnedMeshRenderer _scope2Renderer;
    [SerializeField] private SkinnedMeshRenderer _silencerRenderer;
    [SerializeField] private GameObject _scope1RenderMesh;
    [SerializeField] private GameObject _scope2RenderMesh;
    [SerializeField] private SpriteRenderer _scope1SpriteRenderer;
    [SerializeField] private SpriteRenderer _scope2SpriteRenderer;

    public bool IsEnabledScope => _isEnabledScope;
    public bool IsEnabledScope1 => _isEnabledScope1;
    public bool IsEnabledScope2 => _isEnabledScope2;
    public bool IsEnabledSilencer => _isEnabledSilencer;

    private void Start()
    {
        if (_scopeRenderer != null)
        {
            if (_isEnabledScope)
                _scopeRenderer.enabled = true;
            else
                _scopeRenderer.enabled = false;
        }

        if (_scope1Renderer != null && _isEnabledScope1)
        {
            if (_isEnabledScope1)
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
        else if (_scope2Renderer != null)
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

        if (_silencerRenderer != null)
        {
            if ((IsEnabledSilencer))
                _silencerRenderer.enabled = true;
            else
                _silencerRenderer.enabled = false;
        }
    }

    public void Aim(bool isAiming)
    {
        if (_scope1Renderer)
        {
            if (IsEnabledScope1)
                _scope1SpriteRenderer.enabled = isAiming;
        }

        if (_scope2Renderer)
        {
            if (IsEnabledScope2)
                _scope2SpriteRenderer.enabled = isAiming;
        }
    }
}
