using UnityEngine;

public class Hiding : MonoBehaviour
{
    [SerializeField] private float _delay;

    private void OnEnable()
    {
        Invoke(nameof(Execute), _delay);
    }

    private void Execute()
    {
        gameObject.SetActive(false);
    }
}
