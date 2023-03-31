using Agava.YandexGames;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class GettingCloudSaving : MonoBehaviour
{
    private string _jsonData;
    private List<CloudSaving> _datas;

    private void OnEnable()
    {
        PlayerAccount.GetPlayerData((data) => _jsonData = data);
        _datas = JsonConvert.DeserializeObject<List<CloudSaving>>(_jsonData);
    }

    public bool Try(out CloudSaving[] cloudSaving)
    {
        cloudSaving = _datas != null ? _datas.ToArray() : null;
        return cloudSaving != null;
    }
}
