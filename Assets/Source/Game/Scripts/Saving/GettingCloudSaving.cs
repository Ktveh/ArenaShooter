using Agava.YandexGames;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class GettingCloudSaving : MonoBehaviour
{
    private string _jsonData;
    private List<CloudSaving> _datas;

    public bool IsSuccess { get; private set; }
    public bool IsError { get; private set; }

    private void OnEnable()
    {
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
        IsSuccess = true;
    }

    private void OnError(string error)
    {
        IsError = true;
    }
}
