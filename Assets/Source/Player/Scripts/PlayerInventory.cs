using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private ButtonBuyingAmmo _buttonBuyingAmmo;
    [SerializeField] private WeaponSaving _weaponSaving;

    private Dictionary<Weapon.Types, uint> _ammo;

    public event UnityAction Changed;
    public event UnityAction<Item, uint> Taked;

    private void Awake()
    {
        if (_ammo == null)
        {
            _ammo = new Dictionary<Weapon.Types, uint>()
            {
                {Weapon.Types.Pistol, 1000},
                {Weapon.Types.SMG, (uint)_weaponSaving.GetAmountAmmo(Weapon.Types.SMG) },
                {Weapon.Types.Rifle, (uint)_weaponSaving.GetAmountAmmo(Weapon.Types.Rifle) },
                {Weapon.Types.SniperRifle, (uint)_weaponSaving.GetAmountAmmo(Weapon.Types.SniperRifle) },
                {Weapon.Types.Shotgun, (uint)_weaponSaving.GetAmountAmmo(Weapon.Types.Shotgun) },
                {Weapon.Types.Grenade, (uint)_weaponSaving.GetAmountAmmo(Weapon.Types.Grenade) },
            };
        }
    }

    private void OnEnable()
    {
        _buttonBuyingAmmo.Buyed += OnBuyed;
    }

    private void OnDisable()
    {
        _buttonBuyingAmmo.Buyed += OnBuyed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Item item) && item.Type != Item.Types.Drug)
        {
            switch(item.Type)
            {
                case Item.Types.SMG:
                    _ammo[Weapon.Types.SMG] += item.Amount;
                    break;
                case Item.Types.Rifle:
                    _ammo[Weapon.Types.Rifle] += item.Amount;
                    break;
                case Item.Types.SniperRifle:
                    _ammo[Weapon.Types.SniperRifle] += item.Amount;
                    break;
                case Item.Types.Shotgun:
                    _ammo[Weapon.Types.Shotgun] += item.Amount;
                    break;
                case Item.Types.Grenade:
                    _ammo[Weapon.Types.Grenade] += item.Amount;
                    break;
            }

            item.gameObject.SetActive(false);
            Changed?.Invoke();
            Taked?.Invoke(item, item.Amount);
        }
    }

    public bool TryGetAmmo(Weapon.Types type, uint needAmount, out uint ammo)
    {
        ammo = 0;

        if (_ammo[type] == 0)
            return false;

        if (_ammo[type] < needAmount) 
        {
            ammo = _ammo[type];
            _ammo[type] = 0;
        }
        else
        {
            ammo = needAmount;
            _ammo[type] -= ammo;
        }

        Changed?.Invoke();
        return true;
    }

    public uint GetAmountAmmo(Weapon.Types type)
    {
        return _ammo[type];
    }

    public Weapon.Types[] GetTypeAmmo()
    {
        List<Weapon.Types> types = new List<Weapon.Types>();

        foreach (Weapon.Types ammo in _ammo.Keys)
        {
            if(ammo != Weapon.Types.Grenade)
                types.Add(ammo);
        }

        return types.ToArray();
    }

    private void OnBuyed(Weapon.Types type, uint amount)
    {
        _ammo[type] += amount;
        Changed?.Invoke();
    }
}
