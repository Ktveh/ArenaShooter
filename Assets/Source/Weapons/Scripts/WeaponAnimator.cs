using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponAccessories))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Weapon))]
public class WeaponAnimator : MonoBehaviour
{
	private const int Layer1 = 1;
	private const string Holster = "Holster";
	private const string Walking = "Walk";
	private const string Running = "Run";
	private const string FireNonScoping = "Fire";
	private const string AimFireScope = "Aim Fire";
	private const string AimFireScope1 = "Aim Fire Scope 2";
	private const string AimFireScope2 = "Aim Fire Scope 3";
	private const string AimScope = "Aim";
	private const string AimScope1 = "Aim Scope 2";
	private const string AimScope2 = "Aim Scope 3";
	private const string ReloadOutOfAmmo = "Reload Out Of Ammo";
	private const string ReloadAmmoLeft = "Reload Ammo Left";
	private const string ReloadOpen = "Reload Open";
	private const string ReloadClose = "Reload Close";
	private const string InsertShell = "Insert Shell";
	private const string OutOfAmmoSlider = "Out Of Ammo Slider";
	private const string GrenadeThrow = "GrenadeThrow";

	private Animator _animator;
	private WeaponAccessories _weaponAccessories;
	private Weapon _weapon;
	private bool _isReloadingShotgun;

	public bool IsRealoding => _animator.GetCurrentAnimatorStateInfo(0).IsName(ReloadOutOfAmmo) || _animator.GetCurrentAnimatorStateInfo(0).IsName(ReloadAmmoLeft) || _isReloadingShotgun;

	public event UnityAction Reloaded;

	private void Awake()
    {
        _weaponAccessories = GetComponent<WeaponAccessories>();
		_animator = GetComponent<Animator>();
		_weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        if(_weapon.IsReloadingStarted)
			if(IsRealoding == false)
					Reloaded?.Invoke();
	}

	public void Walk(bool isWalking)
    {
		_animator.SetBool(Walking, isWalking);
	}

	public void Run(bool isRunning)
    {
		_animator.SetBool(Running, isRunning);
	}

	public void Fire(bool isAiming)
	{
		if (isAiming == false)
		{
			_animator.Play(FireNonScoping, 0, 0f);
		}
		else
		{
			if (_weaponAccessories.IsEnabledScope1 == false && _weaponAccessories.IsEnabledScope2 == false)
				_animator.Play(AimFireScope, 0, 0f);
			else if (_weaponAccessories.IsEnabledScope1)
				_animator.Play(AimFireScope1, 0, 0f);
			else if (_weaponAccessories.IsEnabledScope2)
				_animator.Play(AimFireScope2, 0, 0f);
		}
	}

	public void Aim(bool isAiming)
    {
		if (_weaponAccessories.IsEnabledScope1 == false && _weaponAccessories.IsEnabledScope2 == false)
			_animator.SetBool(AimScope, isAiming);
		
		if (_weaponAccessories.IsEnabledScope1)
			_animator.SetBool(AimScope1, isAiming);
		
		if (_weaponAccessories.IsEnabledScope2)
			_animator.SetBool(AimScope2, isAiming);
	}

	public void ThrowGrenade()
    {
		_animator.Play(GrenadeThrow, 0, 0.0f);
	}

	public void MoveBreechBlock(bool isOpening, float valueLayer)
    {
		_animator.SetBool(OutOfAmmoSlider, isOpening);
		_animator.SetLayerWeight(Layer1, valueLayer);
	}

	public void ChangeLayerWeight(int layer, float value)
    {
		_animator.SetLayerWeight(layer, value);
	}

	public void Hide(bool isHiding)
    {
		_animator.SetBool(Holster, isHiding);
	}

	public void Reload(bool isOutOfAmmo)
    {
		Coroutine coroutine = null;

		if (_weapon.Type == Weapon.Types.Shotgun)
		{
			_isReloadingShotgun = true;
			if (coroutine == null)
                StartCoroutine(ReloadShotgun(_weapon.NeedAmountAmmo));
        }
		else
		{
			if (isOutOfAmmo)
				_animator.Play(ReloadOutOfAmmo, 0, 0f);
			else
				_animator.Play(ReloadAmmoLeft, 0, 0f);
		}
	}

	private IEnumerator ReloadShotgun(uint amount)
    {
		bool isOpening = false;

		while (_isReloadingShotgun)
        {
			if (isOpening == false)
			{
				isOpening = true;
				_animator.Play(ReloadOpen, 0, 0f);
                yield return new WaitForSeconds(_weapon.DuartionReloadingOpen);
            }

			for (int i = 0; i < amount; i++)
            {
				_animator.Play(InsertShell, 0, 0f);
                yield return new WaitForSeconds(_weapon.DuartionInsertShell);
            }

			_animator.Play(ReloadClose, 0, 0f);
            yield return new WaitForSeconds(_weapon.DuartionReloadingClose);
            _isReloadingShotgun = false;
		}
    }
}
