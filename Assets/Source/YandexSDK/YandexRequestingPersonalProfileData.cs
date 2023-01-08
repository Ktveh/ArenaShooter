using Agava.YandexGames;
using UnityEngine;

public class YandexRequestingPersonalProfileData : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
    }
}
