using UnityEngine;

public class WeaponHolster : MonoBehaviour
{
    [SerializeField] private WeaponAnimator _weaponAnimator;
    [SerializeField] private WeaponSound _weaponSound;

    public void Hide(bool isHiding)
    {
        //_weaponAnimator.Hide(isHiding);
        _weaponSound.Hide(isHiding);
    }
}
