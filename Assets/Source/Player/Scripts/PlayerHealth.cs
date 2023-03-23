using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    //private const float DefaultValue = 100f;
    private const float DefaultValue = float.MaxValue;

    private float _value;

    public event UnityAction<float> Changed;
    public event UnityAction TookDamage;
    public event UnityAction Deaded;
    public event UnityAction<Item, uint> Taked;

    private void Start()
    {
        _value = DefaultValue;
        Changed?.Invoke(_value);
    }

    public void Take(float damage)
    {
        _value -= damage;
        Changed?.Invoke(_value);
        TookDamage?.Invoke();

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
                Changed?.Invoke(_value);
                Taked?.Invoke(item, item.Amount);

                if (_value > DefaultValue)
                    _value = DefaultValue;
            }
        }
    }
}
