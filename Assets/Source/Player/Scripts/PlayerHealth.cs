using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    private const float DefaultValue = 100f;

    private float _value = DefaultValue;

    public event UnityAction Deaded;

    public void Take(float damage)
    {
        _value -= damage;

        if (_value <= 0)
            Deaded?.Invoke();
    }

    public void Reset()
    {
        _value = DefaultValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Item item) && (item.Type == Item.Types.Drug))
        {
            if (_value < DefaultValue)
            {
                item.gameObject.SetActive(false);
                _value += item.Amount;

                if (_value > DefaultValue)
                    _value = DefaultValue;
            }
        }
    }
}
