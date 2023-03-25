using UnityEngine;

public class CreatingSoundTarget : MonoBehaviour
{
    [SerializeField] private SoundTarget _template;
    [SerializeField] private float _radius;
    [SerializeField] private float _duration;

    private void OnEnable()
    {
        SoundTarget soundTarget = Instantiate(_template, transform.position, transform.rotation);
        soundTarget.SetRadius(_radius);
        soundTarget.SetDuration(_duration);
        enabled = false;
    }
}
