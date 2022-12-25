using UnityEngine;

public class WeaponParticle : MonoBehaviour
{
    [SerializeField] private float _delay;

    private void OnEnable()
    {
        Invoke(nameof(Disable), _delay);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
