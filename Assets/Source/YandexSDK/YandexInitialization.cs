#pragma warning disable

using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class YandexInitialization : MonoBehaviour
{
    public event UnityAction PlayerAuthorizated;
    public event UnityAction Completed;

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

        yield return YandexGamesSdk.Initialize();

        Completed?.Invoke();

        if(PlayerAccount.IsAuthorized)
            PlayerAuthorizated?.Invoke();
    }
}