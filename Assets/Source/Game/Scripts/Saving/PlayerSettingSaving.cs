using UnityEngine;

public class PlayerSettingSaving : MonoBehaviour
{
    private const string CameraSensitivity = "CameraSensitivity";

    public float CurrentCameraSensitivity => PlayerPrefs.GetFloat(CameraSensitivity);

    public void Set(float cameraSensitivity)
    {
        PlayerPrefs.SetFloat(CameraSensitivity, cameraSensitivity);
    }
}
