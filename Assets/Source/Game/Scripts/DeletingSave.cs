using UnityEngine;

public class DeletingSave : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerPrefs.DeleteAll();
    }
}
