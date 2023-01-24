using UnityEngine;

public class LevelScore : MonoBehaviour
{
    [SerializeField] private PlayerZombieKillCounter _playerZombieKillCounter;
    [SerializeField] private PlayerWeaponSelecting _playerWeaponSelecting;

    public int AmountKilledZombie { get; private set; }
    public int AmountHited { get; private set; }
    public int AmountHitedHead { get; private set; }
    public int AmountShot { get; private set; }
    public float HitAccuracy => ((float)AmountHited + (float)AmountHitedHead) / (float)AmountShot * 100;

    private void OnEnable()
    {
        _playerZombieKillCounter.Changed += OnChanged;
        _playerWeaponSelecting.Selected += OnSelected;
    }

    private void OnDisable()
    {
        _playerZombieKillCounter.Changed -= OnChanged;
        _playerWeaponSelecting.Selected -= OnSelected;
    }

    private void OnSelected()
    {
        if (_playerWeaponSelecting.LastWeapon != null)
        {
            if (_playerWeaponSelecting.LastWeapon.TryGetComponent(out WeaponShooting lastWeaponShooting))
            {
                lastWeaponShooting.Shooted -= OnShooted;
                lastWeaponShooting.Hited -= OnHited;
                lastWeaponShooting.HitedInHead -= OnHitedInHead;
            }
        }

        if (_playerWeaponSelecting.CurrentWeapon.TryGetComponent(out WeaponShooting currentWeaponShooting))
        {
            currentWeaponShooting.Shooted += OnShooted;
            currentWeaponShooting.Hited += OnHited;
            currentWeaponShooting.HitedInHead += OnHitedInHead;
        }
    }

    private void OnShooted()
    {
        ++AmountShot;
    }
    
    private void OnHited()
    {
        ++AmountHited;
    }
    
    private void OnHitedInHead()
    {
        ++AmountHitedHead;
    }

    private void OnChanged(int value)
    {
        AmountKilledZombie = value;
    }
}
