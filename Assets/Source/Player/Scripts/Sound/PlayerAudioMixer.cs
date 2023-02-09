using UnityEngine;

[RequireComponent(typeof(StarterAssets.FirstPersonController))]
[RequireComponent(typeof(WalkingSound))]
[RequireComponent(typeof(JumpingSound))]
public class PlayerAudioMixer : MonoBehaviour
{
    private StarterAssets.FirstPersonController _controller;
    private WalkingSound _walkingSound;
    private JumpingSound _jumpingSound;

    private void Start()
    {
        _controller = GetComponent<StarterAssets.FirstPersonController>();
        _walkingSound = GetComponent<WalkingSound>();
        _jumpingSound = GetComponent<JumpingSound>();
    }

    private void Update()
    {
        if (_controller.Grounded)
            _walkingSound.Play(_controller.IsWalking, _controller.IsRunning);
        else
            _walkingSound.Stop();

        _jumpingSound.Play(_controller.Grounded);
    }
}
