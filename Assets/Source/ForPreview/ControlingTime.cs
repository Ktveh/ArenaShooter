using UnityEngine;

public class ControlingTime : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 1f;
        }
    }
}
