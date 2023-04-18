using UnityEngine;
using CrazyGames;
using UnityEngine.Events;

public class CrazyInitialization : MonoBehaviour
{
    public event UnityAction<string> GetedDevice;

    private void Start()
    {
        CrazySDK.Instance.GetUserInfo(OnSuccess);
    }

    private void OnSuccess(UserInfo info)
    {
        GetedDevice?.Invoke(info.device.type);
    }
}
