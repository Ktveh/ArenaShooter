using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Types _type;
    [SerializeField] private uint _amount;

    public Types Type => _type;
    public uint Amount => _amount;

    public enum Types
    {
        SMG,
        Rifle,
        SniperRifle,
        Shotgun,
        GrenadeLauncher,
        Grenade,
        Drug
    }
}
