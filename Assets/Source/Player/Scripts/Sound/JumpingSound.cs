using UnityEngine;

public class JumpingSound : MonoBehaviour
{
    private const float MinPitch = 0.7f;
    private const float MaxPitch = 1f;

    [SerializeField] private AudioSource _jumping;

    private bool _isJumped;

    public void Play(bool isGrounded)
    {
        if (isGrounded == false)
            _isJumped = true;

        if (_isJumped)
        {
            if (isGrounded)
            {
                if (_jumping.isPlaying == false)
                {
                    _jumping.pitch = Random.Range(MinPitch, MaxPitch);
                    _jumping.Play();
                }

                _isJumped = false;
            }
        }

    }
}
