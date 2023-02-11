using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIVirtualTouchZone : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [System.Serializable]
    public class Event : UnityEvent<Vector2> { }

    [Header("Rect References")]
    public RectTransform containerRect;
    public RectTransform handleRect;

    [Header("Settings")]
    public bool clampToMagnitude;
    public float magnitudeMultiplier = 1f;
    public float multiplier = 100f;
    public float deadZone = 0.05f;
    public float lowDistance = 0.1f;
    public float mediumDistance = 0.5f;
    public float highDistance = 2f;
    public float lowSpeed = 0.2f;
    public float mediumSpeed = 0.5f;
    public float highSpeed = 10f;
    public bool invertXOutputValue;
    public bool invertYOutputValue;

    //Stored Pointer Values
    private Vector2 pointerDownPosition;
    private Vector2 currentPointerPosition;
    private Vector2 velocity = Vector2.zero;
    private float speed = 0.3f;
    private float acceleration;
    private bool isDrag;

    private float distance => Vector2.Distance(pointerDownPosition, currentPointerPosition) / multiplier;

    [Header("Output")]
    public Event touchZoneOutputEvent;

    private void Update()
    {
        if (isDrag)
            pointerDownPosition = Vector2.SmoothDamp(pointerDownPosition, currentPointerPosition, ref velocity, speed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out pointerDownPosition);

        if(handleRect)
        {
            UpdateHandleRectPosition(pointerDownPosition);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out currentPointerPosition);
        
        Vector2 positionDelta = GetDeltaBetweenPositions(pointerDownPosition, currentPointerPosition);

        Vector2 clampedPosition = ClampValuesToMagnitude(positionDelta);
        
        Vector2 outputPosition = ApplyInversionFilter(clampedPosition);

        if ((distance > lowDistance) && (distance < mediumDistance))
            acceleration = lowSpeed;
        else if((distance > mediumDistance) && (distance < highDistance))
            acceleration = mediumSpeed;
        else if(distance > highDistance)
            acceleration += Time.deltaTime * highSpeed;

        if (distance > deadZone)
            OutputPointerEventValue(outputPosition * magnitudeMultiplier * acceleration);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
        pointerDownPosition = Vector2.zero;
        currentPointerPosition = Vector2.zero;

        OutputPointerEventValue(Vector2.zero);

        if(handleRect)
        {
            UpdateHandleRectPosition(Vector2.zero);
        }
    }

    void OutputPointerEventValue(Vector2 pointerPosition)
    {
        touchZoneOutputEvent.Invoke(pointerPosition);
    }

    void UpdateHandleRectPosition(Vector2 newPosition)
    {
        handleRect.anchoredPosition = newPosition;
    }

    void SetObjectActiveState(GameObject targetObject, bool newState)
    {
        targetObject.SetActive(newState);
    }

    Vector2 GetDeltaBetweenPositions(Vector2 firstPosition, Vector2 secondPosition)
    {
        return secondPosition - firstPosition;
    }

    Vector2 ClampValuesToMagnitude(Vector2 position)
    {
        return Vector2.ClampMagnitude(position, 1);
    }

    Vector2 ApplyInversionFilter(Vector2 position)
    {
        if(invertXOutputValue)
        {
            position.x = InvertValue(position.x);
        }

        if(invertYOutputValue)
        {
            position.y = InvertValue(position.y);
        }

        return position;
    }

    float InvertValue(float value)
    {
        return -value;
    }
    
}
