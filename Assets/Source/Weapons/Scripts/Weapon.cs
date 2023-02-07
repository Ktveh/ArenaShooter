using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponAnimator))]
[RequireComponent(typeof(ThrowingGrenade))]
[RequireComponent(typeof(WeaponShooting))]
[RequireComponent(typeof(WeaponHolster))]
[RequireComponent(typeof(WeaponReloading))]
[RequireComponent(typeof(WeaponBreechBlock))]
[RequireComponent(typeof(WeaponSound))]
public class Weapon : MonoBehaviour
{
	[SerializeField] private Sprite _icon;
	[SerializeField] private Types _type;
	[SerializeField] private int _priceAmmo;
	[SerializeField] private uint _maxAmountAmmo;
	[SerializeField] private float _nextShotDelay;
	[SerializeField] private float _forceRecoil;
	[SerializeField] private bool _isSingleShootMod;
	[Header("Shotgun settings")]
	[SerializeField] private float _durationReloadingOpen;
	[SerializeField] private float _durationInsertShell;
	[SerializeField] private float _durationReloadingClose;

	private Getting _getting;
	private WeaponAnimator _weaponAnimator;
	private ThrowingGrenade _throwingGrenade;
	private WeaponShooting _weaponShooting;
	private WeaponHolster _weaponHolster;
	private WeaponReloading _weaponReloading;
	private WeaponBreechBlock _weaponBreechBlock;
	private WeaponSound _weaponSound;
	private StarterAssets.FirstPersonController _personController;
	private PlayerScopeOpening _playerScopeOpening;
	private PlayerShooting _playerShooting;
	private PlayerDroppingGrenade _playerDroppingGrenade;
	private PlayerWeaponReloading _playerWeaponReloading;
	private PlayerInventory _playerInventory;
	private Camera _weaponCamera;
	private bool _isBreechBlockOpen;
	private bool _isOpeningScope;
	private bool _isMovingMovableHandguard;
	//private bool _isHasHolstered = false;

	public Camera WeaponCamera => _weaponCamera;
	public Sprite Icon => _icon;
	public Types Type => _type;
	public int PriceAmmo => _priceAmmo;
	public uint CurrentAmountAmmo { get; private set; }
	public uint MaxAmountAmmo => _maxAmountAmmo;
	public uint NeedAmountAmmo => _maxAmountAmmo - CurrentAmountAmmo;
	public uint AmountAmmoReceived { get; private set; }
	public float ForceRecoil => _forceRecoil;
	public float DuartionReloadingOpen => _durationReloadingOpen;
	public float DuartionInsertShell => _durationInsertShell;
	public float DuartionReloadingClose => _durationReloadingClose;
	public bool IsShooting { get; private set; }
	public bool IsScoping => _isScoping;
	public bool IsReloadingStarted { get; private set; }
	public bool IsReloading => _weaponAnimator.IsRealoding;

	private bool _isScoping => _isOpeningScope && IsReloading == false && _isRunning == false;
	private bool _isRunning => _personController.IsRunning;
	private bool _isOutOfAmmo => CurrentAmountAmmo == 0;

	public event UnityAction Shooted;
	public event UnityAction Reloaded;

	public enum Types
	{
		Pistol,
		SMG,
		Rifle,
		SniperRifle,
		Shotgun,
		MiniGun,
		GrenadeLauncher,
		Flamethrower,
		Grenade
	}

	private void Awake()
	{
		_getting = GetComponentInParent<Getting>();
		_weaponAnimator = GetComponent<WeaponAnimator>();
		_throwingGrenade = GetComponent<ThrowingGrenade>();
		_weaponShooting = GetComponent<WeaponShooting>();
		_weaponHolster = GetComponent<WeaponHolster>();
		_weaponReloading = GetComponent<WeaponReloading>();
		_weaponBreechBlock = GetComponent<WeaponBreechBlock>();
		_weaponSound = GetComponent<WeaponSound>();
		_personController = _getting.PersonController;
		_playerScopeOpening = _getting.PlayerScopeOpening;
		_playerShooting = _getting.PlayerShooting;
		_playerDroppingGrenade = _getting.PlayerDroppingGrenade;
		_playerWeaponReloading = _getting.PlayerWeaponReloading;
		_playerInventory = _getting.PlayerInventory;
		_weaponCamera = _getting.WeaponCamera;
	}

	private void Start()
	{
		if (_playerInventory.TryGetAmmo(_type, _maxAmountAmmo, out uint ammo))
			CurrentAmountAmmo = ammo;
		else
			CheckBreechBlock();

		Reloaded?.Invoke();
	}

	private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.E))
		//{
		//	if (_isHasHolstered)
		//		_isHasHolstered = false;
		//	else
		//		_isHasHolstered = true;

		//	_weaponHolster.Hide(_isHasHolstered);
		//}

		if (_isRunning)
			OnClosedScope();

		_weaponAnimator.Walk(_personController.IsWalking && _isScoping == false);
		_weaponAnimator.Run(_isRunning);
	}

	private void OnEnable()
	{
		_weaponAnimator.Reloaded += OnFinishedAnimation;
		_weaponBreechBlock.Returned += OnReturned;
		_playerScopeOpening.Scoped += OnOpenedScope;
		_playerScopeOpening.NonScoped += OnClosedScope;
		//_playerDroppingGrenade.Actioned += OnDropedGrenade;
		_playerShooting.Shooted += OnShooted;
		_playerShooting.NonShooted += OnNonShooted;
		_playerWeaponReloading.Reloaded += OnReload;

		_weaponHolster.Hide(false);

		if (_type == Types.Shotgun)
			_isMovingMovableHandguard = false;
	}

	private void OnDisable()
	{
		_weaponAnimator.Reloaded -= OnFinishedAnimation;
		_weaponBreechBlock.Returned -= OnReturned;
		_playerScopeOpening.Scoped -= OnOpenedScope;
		_playerScopeOpening.NonScoped -= OnClosedScope;
		//_playerDroppingGrenade.Actioned -= OnDropedGrenade;
		_playerShooting.Shooted -= OnShooted;
		_playerShooting.NonShooted -= OnNonShooted;
		_playerWeaponReloading.Reloaded -= OnReload;

		_isOpeningScope = false;
	}

	//public void Hide()
	//   {
	//	_weaponHolster.Hide(true);
	//}

	private void OnFinishedAnimation()
	{
		CurrentAmountAmmo += AmountAmmoReceived;
		IsReloadingStarted = false;
		CheckBreechBlock();
		Reloaded?.Invoke();
	}

	private void OnReturned()
	{
		_isBreechBlockOpen = false;
	}

	private void CheckBreechBlock()
	{
		if (_isOutOfAmmo)
			_weaponAnimator.MoveBreechBlock(true, 1);
		else
			_weaponAnimator.ChangeLayerWeight(1, 0);
	}

	private void OnOpenedScope()
	{
		_isOpeningScope = true;
		_weaponAnimator.Aim(_isScoping);
		_weaponSound.Aim(_isScoping);
	}

	private void OnClosedScope()
	{
		_isOpeningScope = false;
		_weaponAnimator.Aim(_isScoping);
		_weaponSound.Aim(_isScoping);
	}

	private void OnDropedGrenade()
	{
		_throwingGrenade.enabled = true;
	}

	private void OnShooted()
	{
		if ((IsReloading == false) && (_isRunning == false))
		{
			if (_isSingleShootMod && _type != Types.Shotgun)
			{
				Shoot();
			}
			else if (_isSingleShootMod && _type == Types.Shotgun)
			{
				if (_isMovingMovableHandguard == false)
					StartCoroutine(ShootShotgun());
			}
			else
			{
				IsShooting = true;
				StartCoroutine(ShootAuto());
			}
		}
	}

	private void OnNonShooted()
	{
		IsShooting = false;
	}

	private void OnReload()
	{
		if ((IsReloading == false) && (_isRunning == false))
		{
			if (_playerInventory.TryGetAmmo(_type, NeedAmountAmmo, out uint ammo))
			{
				AmountAmmoReceived = ammo;
				IsReloadingStarted = true;
				_weaponReloading.Reload(_isOutOfAmmo);

				if (_isBreechBlockOpen == false)
				{
					_isBreechBlockOpen = true;
					_weaponBreechBlock.enabled = true;
				}
			}
		}
	}

	private void Shoot()
	{
		if (_isOutOfAmmo)
		{
			OnReload();
		}
		else
		{
			--CurrentAmountAmmo;
			_weaponShooting.LaunchBullet(_isScoping);
			Shooted?.Invoke();
		}

		CheckBreechBlock();
	}

	private IEnumerator ShootAuto()
	{
		while (IsShooting && (IsReloading == false) && (_isRunning == false))
		{
			Shoot();
			yield return new WaitForSeconds(_nextShotDelay);
		}
	}

	private IEnumerator ShootShotgun()
	{
		_isMovingMovableHandguard = true;
		Shoot();

		while (_isMovingMovableHandguard)
		{
			yield return new WaitForSeconds(_nextShotDelay);
			_isMovingMovableHandguard = false;
		}
	}
}
