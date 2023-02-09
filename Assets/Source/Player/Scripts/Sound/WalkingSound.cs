using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    private const float MinPitch = 0.7f;
    private const float MaxPitch = 1f;

    [SerializeField] private AudioSource _walkingSound;
    [SerializeField] private AudioSource _runingSound;

    public void Play(bool isWalking, bool isRuning)
    {
        if (isWalking && isRuning == false)
        {
            _runingSound.Stop();
            _walkingSound.pitch = Random.Range(MinPitch, MaxPitch);

            if (_walkingSound.isPlaying == false)
                _walkingSound.Play();
        }
        else if (isRuning)
        {
            _walkingSound.Stop();
            _runingSound.pitch = Random.Range(MinPitch, MaxPitch);

            if (_runingSound.isPlaying == false)
                _runingSound.Play();
        }
        else
        {
            Stop();
        }
    }

    public void Stop()
    {
        _walkingSound.Stop();
        _runingSound.Stop();
    }
}
