using UnityEngine;

public class WeaponFollowingCamera : MonoBehaviour
{
    private const float SwayAmount = 0.02f;
    private const float MaxSwayAmount = 0.06f;
    private const float SwaySmoothValue = 10f;
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    private Getting _getting;
    private Vector3 _initialSwayPosition;

    private void Start()
    {
        _getting = GetComponentInChildren<Getting>();
        enabled = _getting.Game.IsMobile ? false : true;
        _initialSwayPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        float movementX = -Input.GetAxis(MouseX) * SwayAmount;
        float movementY = -Input.GetAxis(MouseY) * SwayAmount;
        movementX = Mathf.Clamp(movementX, -MaxSwayAmount, MaxSwayAmount);
        movementY = Mathf.Clamp(movementY, -MaxSwayAmount, MaxSwayAmount);
        Vector3 finalSwayPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalSwayPosition + _initialSwayPosition, Time.deltaTime * SwaySmoothValue);
    }
}
