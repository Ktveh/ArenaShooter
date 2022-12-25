using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponAnimator))]
public class WeaponBreechBlock : MonoBehaviour
{
	[SerializeField] private float _delay = 1.58f;

	private WeaponAnimator _weaponAnimator;

	public event UnityAction Returned;

    private void Awake()
    {
        _weaponAnimator = GetComponent<WeaponAnimator>();
        enabled = false;
    }

    private void OnEnable()
    {
        StartCoroutine(HandgunSliderBackDelay());
    }

    private IEnumerator HandgunSliderBackDelay()
	{
		yield return new WaitForSeconds(_delay);

		_weaponAnimator.MoveBreechBlock(false, 0);
		Returned?.Invoke();
        enabled = false;
	}
}
