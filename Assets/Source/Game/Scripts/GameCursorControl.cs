using UnityEngine;

public class GameCursorControl : MonoBehaviour
{
    public bool IsDisable => (Cursor.visible == false) && (Cursor.lockState == CursorLockMode.Locked);

    public void Enable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void Disable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
