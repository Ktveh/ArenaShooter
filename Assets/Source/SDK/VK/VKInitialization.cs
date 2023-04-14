#pragma warning disable

using DungeonGames.VKGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VKInitialization : MonoBehaviour
{
    public event UnityAction PlayerAuthorizated;
    public event UnityAction Completed;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        if (VKGamesSdk.Initialized == false)
        {
            yield return VKGamesSdk.Initialize(onSuccessCallback: OnSDKInitilized);
        }
        else
        {
            Completed?.Invoke();
            yield break;
        }
    }

    private void OnSDKInitilized()
    {
        Completed?.Invoke();
    }
}