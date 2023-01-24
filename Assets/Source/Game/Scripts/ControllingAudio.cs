using UnityEngine;

public class ControllingAudio : MonoBehaviour
{
    private const float MinValue = 0f;
    private const float MaxValue = 1f;

    private void OnEnable()
    {
        AudioListener.pause = false;
        AudioListener.volume = MaxValue;
    }

    private void OnDisable()
    {
        AudioListener.pause = true;
        AudioListener.volume = MinValue;
    }
}
