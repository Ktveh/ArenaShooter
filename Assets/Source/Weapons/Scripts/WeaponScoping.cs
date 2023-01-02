using UnityEngine;

[RequireComponent(typeof(WeaponAccessories))]
[RequireComponent(typeof(Weapon))]
public class WeaponScoping : MonoBehaviour
{
	private const float _aimingPositionY = 0.587f;
	private const float _defaultPositionY = 0.6f;

	[SerializeField] private float _aimingSpeed = 15f;
	[SerializeField] private float _defaultFieldOfView = 40f;
	[Range(5, 40)]
	[SerializeField] private float _scopeFieldOfView = 16;
	[Range(5, 40)]
	[SerializeField] private float _scope1FieldOfView = 25;
	[Range(5, 40)]
	[SerializeField] private float _scope2FieldOfView = 20;

	private Weapon _weapon;
    private Camera _weaponCamera;
    private WeaponAccessories _weaponAccessories;

    private void Start()
    {
		_weapon = GetComponent<Weapon>();
        _weaponCamera = _weapon.WeaponCamera;
        _weaponAccessories = GetComponent<WeaponAccessories>();
	}

    private void Update()
    {
		if (_weapon.IsScoping)
        {
			if (_weaponAccessories.IsEnabledScope && (_weaponAccessories.IsEnabledScope1 == false) && (_weaponAccessories.IsEnabledScope2 == false))
				_weaponCamera.fieldOfView = Mathf.Lerp(_weaponCamera.fieldOfView, _scopeFieldOfView, _aimingSpeed * Time.deltaTime);

			if (_weaponAccessories.IsEnabledScope1)
			{
				_weaponCamera.fieldOfView = Mathf.Lerp(_weaponCamera.fieldOfView, _scope1FieldOfView, _aimingSpeed * Time.deltaTime);
			}

			if (_weaponAccessories.IsEnabledScope2)
			{
				_weaponCamera.fieldOfView = Mathf.Lerp(_weaponCamera.fieldOfView, _scope2FieldOfView, _aimingSpeed * Time.deltaTime);

				if ((_weapon.Type == Weapon.Types.SMG) || (_weapon.Type == Weapon.Types.Rifle))
					_weaponCamera.transform.localPosition = new Vector3(_weaponCamera.transform.localPosition.x, _aimingPositionY, _weaponCamera.transform.localPosition.z);
			}
		}
		else
		{
			_weaponCamera.fieldOfView = Mathf.Lerp(_weaponCamera.fieldOfView, _defaultFieldOfView, _aimingSpeed * Time.deltaTime);

			if ((_weapon.Type == Weapon.Types.SMG) || (_weapon.Type == Weapon.Types.Rifle))
				_weaponCamera.transform.localPosition = new Vector3(_weaponCamera.transform.localPosition.x, _defaultPositionY, _weaponCamera.transform.localPosition.z);
		}
	}
}
