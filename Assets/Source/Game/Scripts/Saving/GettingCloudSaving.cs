using Agava.YandexGames;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class GettingCloudSaving : MonoBehaviour
{
    private string _jsonDatas;
    private List<CloudSaving> _datas;

    private void OnEnable()
    {
        PlayerAccount.GetPlayerData((data) => _jsonDatas = data);
        _datas = JsonConvert.DeserializeObject<List<CloudSaving>>(_jsonDatas);
    }

    public bool Try(out CloudSaving[] cloudSaving)
    {
        cloudSaving = _datas != null ? _datas.ToArray() : null;
        return cloudSaving != null;
    }
}
