using UnityEngine;

public class CheckingGround : MonoBehaviour
{
    public bool IsTrue { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        IsTrue = other != null;
    }

    private void OnTriggerExit(Collider other)
    {
        IsTrue = false;
    }
}
