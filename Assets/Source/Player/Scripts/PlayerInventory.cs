using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<Weapon.Types, uint> _ammo;

    private void Start()
    {
        if (_ammo == null)
        {
            _ammo = new Dictionary<Weapon.Types, uint>()
            {
                {Weapon.Types.SMG, 0 },
                {Weapon.Types.Rifle, 0 },
                {Weapon.Types.SniperRifle, 0 },
                {Weapon.Types.Shotgun, 0 },
                {Weapon.Types.Grenade, 0 },
            };
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Item item))
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
        }
    }

    public bool TryGetAmmo(Weapon.Types type, uint maxAmount, uint remaining, out uint ammo)
    {
        ammo = 0;

        if (_ammo[type] == 0)
            return false;

        _ammo[type] += remaining;

        if (_ammo[type] < maxAmount) 
        {
            ammo = _ammo[type];
            _ammo[type] = 0;
        }
        else
        {
            ammo = maxAmount;
            _ammo[type] -= ammo;
        }

        return true;
    }

    public uint GetAmountAmmo(Weapon.Types type)
    {
        return _ammo[type];
    }
}
