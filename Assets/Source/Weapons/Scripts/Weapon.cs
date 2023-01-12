using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponAnimator))]
[RequireComponent(typeof(WeaponAccessories))]
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
	[SerializeField] private uint _maxAmountAmmo;
	[SerializeField] private float _nextShotDelay;
	[SerializeField] private float _forceRecoil;
	[SerializeField] private bool _isSingleShootMod;

	[Header("Shotgun settings")]
	[SerializeField] private float _durationReloadingOpen;
	[SerializeField] private float _durationInsertShell;
	[SerializeField] private float _durationReloadingClose;

	private Getting _gettingPlayer;
	private WeaponAnimator _weaponAnimator;
	private WeaponAccessories _weaponAccessories;
	private ThrowingGrenade _throwingGrenade;
	private WeaponShooting _weaponShooting;
	private WeaponHolster _weaponHolster;
	private WeaponReloading _weaponReloading;
	private WeaponBreechBlock _weaponBreechBlock;
	private WeaponSound _weaponSound;
	private PlayerMovement _playerMovement;
	private PlayerScopeOpening _playerScopeOpening;
	private PlayerShooting _playerShooting;
	private PlayerDroppingGrenade _playerDroppingGrenade;
	private PlayerWeaponReloading _playerWeaponReloading;
	private PlayerInventory _playerInventory;
	private Camera _weaponCamera;
	private uint _amountAmmoReceived;
	private bool _isBreechBlockOpen;
	private bool _isShooting;
	private bool _isOpeningScope;
	private bool _isMovingMovableHandguard;
	//private bool _isHasHolstered = false;

	public Camera WeaponCamera => _weaponCamera;
	public Sprite Icon => _icon;
	public Types Type => _type;
	public uint CurrentAmountAmmo { get; private set; }
	public uint NeedAmountAmmo => _maxAmountAmmo - CurrentAmountAmmo;
	public float ForceRecoil => _forceRecoil;
	public float DuartionReloadingOpen => _durationReloadingOpen;
	public float DuartionInsertShell => _durationInsertShell;
	public float DuartionReloadingClose => _durationReloadingClose;
	public bool IsShooting => _isShooting;
	public bool IsScoping => _isScoping;
	public bool IsReloadingStarted { get; private set; }
    public bool IsReloading => _weaponAnimator.IsRealoding;

	private bool _isScoping => _isOpeningScope && IsReloading == false && _isRunning == false;
	private bool _isRunning => _playerMovement.IsRunning;
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
		_gettingPlayer = GetComponentInParent<Getting>();
		_weaponAnimator = GetComponent<WeaponAnimator>();
		_weaponAccessories = GetComponent<WeaponAccessories>();
		_throwingGrenade = GetComponent<ThrowingGrenade>();
		_weaponShooting = GetComponent<WeaponShooting>();
		_weaponHolster = GetComponent<WeaponHolster>();
		_weaponReloading = GetComponent<WeaponReloading>();
		_weaponBreechBlock = GetComponent<WeaponBreechBlock>();
		_weaponSound = GetComponent<WeaponSound>();
		_playerMovement = _gettingPlayer.PlayerMovement;
		_playerScopeOpening = _gettingPlayer.PlayerScopeOpening;
		_playerShooting = _gettingPlayer.PlayerShooting;
		_playerDroppingGrenade = _gettingPlayer.PlayerDroppingGrenade;
		_playerWeaponReloading = _gettingPlayer.PlayerWeaponReloading;
		_playerInventory = _gettingPlayer.PlayerInventory;
		_weaponCamera = _gettingPlayer.WeaponCamera;
	}

    private void Start()
    {
		if (_playerInventory.TryGetAmmo(_type, _maxAmountAmmo, CurrentAmountAmmo, out uint ammo))
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

		_weaponAnimator.Walk(_playerMovement.IsWalking && _isScoping == false);
		_weaponAnimator.Run(_isRunning);
	}

    private void OnEnable()
    {
		_weaponAnimator.Reloaded += OnFinishedAnimation;
		_weaponBreechBlock.Returned += OnReturned;
		_playerScopeOpening.Actioned += OnOpenedScope;
		_playerScopeOpening.WithoutActioned += OnClosedScope;
		_playerDroppingGrenade.Actioned += OnDropedGrenade;
		_playerShooting.Actioned += OnShooted;
		_playerShooting.WithoutActioned += OnNonShooted;
		_playerWeaponReloading.Actioned += OnReload;

		_weaponHolster.Hide(false);

		if (_type == Weapon.Types.Shotgun)
			_isMovingMovableHandguard = false;
	}

    private void OnDisable()
    {
		_weaponAnimator.Reloaded -= OnFinishedAnimation;
		_weaponBreechBlock.Returned -= OnReturned;
		_playerScopeOpening.Actioned -= OnOpenedScope;
		_playerScopeOpening.WithoutActioned -= OnClosedScope;
		_playerDroppingGrenade.Actioned -= OnDropedGrenade;
		_playerShooting.Actioned -= OnShooted;
		_playerShooting.WithoutActioned -= OnNonShooted;
		_playerWeaponReloading.Actioned -= OnReload;
	}

	//public void Hide()
 //   {
	//	_weaponHolster.Hide(true);
	//}

	private void OnFinishedAnimation()
    {
		CurrentAmountAmmo = _amountAmmoReceived;
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
		_weaponAccessories.Aim(_isScoping);
		_weaponSound.Aim(_isScoping);
	}
	
	private void OnClosedScope()
    {
		_isOpeningScope = false;
		_weaponAnimator.Aim(_isScoping);
		_weaponAccessories.Aim(_isScoping);
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
			if (_isSingleShootMod && _type != Weapon.Types.Shotgun)
			{
				Shoot();
			}
			else if (_isSingleShootMod && _type == Weapon.Types.Shotgun)
			{
				if(_isMovingMovableHandguard == false)
					StartCoroutine(ShootShotgun());
			}
			else
			{
				_isShooting = true;
				StartCoroutine(ShootAuto());
			}
		}
	}

	private void OnNonShooted()
    {
		_isShooting = false;
	}

	private void OnReload()
    {
		if ((IsReloading == false) && (_isRunning == false))
		{
			if (_playerInventory.TryGetAmmo(_type, _maxAmountAmmo, CurrentAmountAmmo, out uint ammo))
			{
				IsReloadingStarted = true;
				_amountAmmoReceived = ammo;
				_weaponReloading.Reload(_isOutOfAmmo, _type, _amountAmmoReceived);

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
        while (_isShooting && (IsReloading == false) && (_isRunning == false))
        {
			Shoot();
			yield return new WaitForSeconds(_nextShotDelay);
        }
    }

	private IEnumerator ShootShotgun()
	{
		_isMovingMovableHandguard = true;
		Shoot();

		while ((_isMovingMovableHandguard))
		{
			yield return new WaitForSeconds(_nextShotDelay);
			_isMovingMovableHandguard = false;
		}
	}
}