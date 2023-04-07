using Agava.YandexGames;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CheckingSaving))]
public class GettingCloudSaving : MonoBehaviour
{
    private CheckingSaving _checkingSaving;
    private List<CloudSaving> _datas;
    private string _jsonData;

    private void OnEnable()
    {
        _checkingSaving = GetComponent<CheckingSaving>();
        PlayerAccount.GetPlayerData(OnSuccess, OnError);
    }

    public bool Try(out CloudSaving[] cloudSaving)
    {
        if (_jsonData != null)
            _datas = JsonConvert.DeserializeObject<List<CloudSaving>>(_jsonData);

        cloudSaving = _datas != null ? _datas.ToArray() : null;
        return cloudSaving != null;
    }

    private void OnSuccess(string data)
    {
        _jsonData = data;
        _checkingSaving.SetSaving();
    }

    private void OnError(string error)
    {
        _checkingSaving.RecoverScore();
    }
}
