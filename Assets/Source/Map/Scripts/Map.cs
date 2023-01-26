using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    [SerializeField] private ZombieCounter _zombieCounter;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Image _map;
    [SerializeField] private Image _playerIcon;
    [SerializeField] private Image _zombieIcon;
    [SerializeField] private float _scalePosition;

    private List<Image> _zombieIcons = new List<Image>();
    float _side;
    Vector2 _offset;

    private void Start()
    {
        float width = _map.rectTransform.sizeDelta.x;
        float height = _map.rectTransform.sizeDelta.y;

        _map.rectTransform.sizeDelta = width > height ? new Vector2(width, width) : new Vector2(height, height);
        _side = _map.rectTransform.sizeDelta.x;

        _map.rectTransform.anchoredPosition = new Vector2(-_side / 2, -_side / 2);

        for (int i = 0; i < _zombieCounter.StartAmount; i++)
        {
            Image newIcon = Instantiate(_zombieIcon, _map.transform);
            _zombieIcons.Add(newIcon);
        }

        _playerIcon.rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    private void Update()
    {
        _offset = GetPlayerOffset();
        Clear();
        
        List<Vector3> zombies = _zombieCounter.GetPositions();
        for (int i = 0; i < zombies.Count; i++)
        {
            _zombieIcons[i].gameObject.SetActive(true);
            _zombieIcons[i].rectTransform.anchoredPosition = ConvertCoordinates(zombies[i]);
            _zombieIcons[i].rectTransform.localEulerAngles = Vector3.zero;
        }

        _playerIcon.rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    private Vector2 GetPlayerOffset()
    {
        return new Vector2(_playerPosition.position.x, _playerPosition.position.z);
    }

    private Vector2 ConvertCoordinates(Vector3 position)
    {
        Vector3 coordinates = position;
        coordinates -= new Vector3(_offset.x, 0, _offset.y);
        coordinates = Quaternion.Inverse(_playerPosition.rotation) * coordinates;
        coordinates *= _scalePosition;
        Vector3 rectCoordinates = new Vector2(coordinates.x, coordinates.z);
        rectCoordinates = new Vector2(SetClamp(rectCoordinates.x), SetClamp(rectCoordinates.y));
        return rectCoordinates;
    }

    private float SetClamp(float value)
    {
        float halfSide = _side / 2;
        float halfWidthImage = _zombieIcon.rectTransform.sizeDelta.x / 2;
        float maxValue = halfSide - halfWidthImage;
        return Mathf.Clamp(value, -maxValue, maxValue);
    }

    private void Clear()
    {
        foreach (Image icon in _zombieIcons)
        {
            icon.gameObject.SetActive(false);
        }
    }
}
