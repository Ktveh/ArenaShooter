using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    private void Update()
    {
        transform.eulerAngles = new Vector3(transform.rotation.x, Camera.main.transform.eulerAngles.y);
    }
}
