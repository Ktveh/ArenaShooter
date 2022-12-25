using System.Collections;
using UnityEngine;

[RequireComponent(typeof(WeaponAnimator))]
[RequireComponent(typeof(WeaponSound))]
public class WeaponReloading : MonoBehaviour
{
	[SerializeField] private SkinnedMeshRenderer _bulletMeshRenderer;
	[SerializeField] private float _delayShowingBullet = 0.6f;

	private WeaponAnimator _weaponAnimator;
	private WeaponSound _weaponSound;

    private void Awake()
    {
        _weaponAnimator = GetComponent<WeaponAnimator>();
		_weaponSound = GetComponent<WeaponSound>();
    }

    public void Reload(bool isOutOfAmmo, Weapon.Types type, uint amount)
	{
		if (isOutOfAmmo)
		{
			_weaponAnimator.Reload(isOutOfAmmo);
			_weaponSound.Reload(isOutOfAmmo);

			if (_bulletMeshRenderer != null)
			{
				_bulletMeshRenderer.enabled = false;
				StartCoroutine(ShowBulletInMag());
			}
		}
		else
		{
			_weaponAnimator.Reload(isOutOfAmmo);
			_weaponSound.Reload(isOutOfAmmo);

			if (_bulletMeshRenderer != null)
				_bulletMeshRenderer.enabled = true;
		}
	}

	private IEnumerator ShowBulletInMag()
	{
		yield return new WaitForSeconds(_delayShowingBullet);

		_bulletMeshRenderer.enabled = true;
	}
}
