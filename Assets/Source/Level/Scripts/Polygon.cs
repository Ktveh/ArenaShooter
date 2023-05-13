using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Polygon : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rooms;
    [SerializeField] private List<GameObject> _xWalls;
    [SerializeField] private List<GameObject> _zWalls;
    [SerializeField] private List<GameObject> _angleWalls;
    [SerializeField] private GameObject _startRoom;
    [SerializeField] private Transform _player;

    private const int MinRooms = 20;
    private const int MaxRooms = 30;
    private const int XSide = 43;
    private const int ZSide = 43;
    private const float Offset = 10; 

    private int[,] _positions;
    private int _roomCount;
    private Vector2Int _startPosition;

    private void Awake()
    {
        _positions = new int[XSide, ZSide];
        GenerateRooms();
        GenerateWalls();
        Placement();
        _player.transform.position = new Vector3(_startPosition.x * Offset, 1, _startPosition.y * Offset);
    }

    private void GenerateRooms()
    {
        _roomCount = Random.Range(MinRooms, MaxRooms);
        int currentX = 0;
        int currentZ = 0;
        while(currentX % 2 == 0 || currentZ % 2 == 0)
        {
            currentX = Random.Range(0, XSide);
            currentZ = Random.Range(0, ZSide);
        }
        _startPosition.x = currentX;
        _startPosition.y = currentZ;
        _positions[currentX, currentZ] = (int)Room.Room;
        int iteration = 0;
        while (iteration < _roomCount)
        {
            int newXPosition = Random.Range(-1, 2) * 2;
            int newZPosition = Random.Range(-1, 2) * 2;
            if (Mathf.Abs(newXPosition) + Mathf.Abs(newZPosition) == 4)
                continue;
            newXPosition += currentX;
            newZPosition += currentZ;
            if (newXPosition < 0 || newZPosition < 0 || newXPosition >= XSide || newZPosition >= ZSide)
                continue;
            currentX = newXPosition;
            currentZ = newZPosition;
            if (_positions[currentX, currentZ] == 0)
            {
                _positions[currentX, currentZ] = (int)Room.Room;
                iteration++;
            }
        }
    }

    private void GenerateWalls()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int x = 0; x < XSide; x++)
            {
                for (int z = 0; z < ZSide; z++)
                {
                    if (x % 2 != 0 && z % 2 != 0) continue;

                    int previousX = x == 0 ? 0 : _positions[x - 1, z];
                    int nextX = x == XSide - 1 ? 0 : _positions[x + 1, z];
                    int previousZ = z == 0 ? 0 : _positions[x, z - 1];
                    int nextZ = z == ZSide - 1 ? 0 : _positions[x, z + 1];

                    if (i == 0 && x % 2 != 0 && z % 2 == 0)
                    {
                        if (previousZ > 0 && nextZ > 0)
                            _positions[x, z] = (int)Room.Room;
                        else if (previousZ > 0 || nextZ > 0)
                            _positions[x, z] = (int)Room.XWall;
                    }
                    if (i == 0 && x % 2 == 0 && z % 2 != 0)
                    {
                        if (previousX > 0 && nextX > 0)
                            _positions[x, z] = (int)Room.Room;
                        else if (previousX > 0 || nextX > 0)
                            _positions[x, z] = (int)Room.ZWall;
                    }
                    if (i == 1 && x % 2 == 0 && z % 2 == 0)
                    {
                        if (previousX == (int)Room.Room && previousZ == (int)Room.Room &&
                                nextX == (int)Room.Room && nextZ == (int)Room.Room)
                            _positions[x, z] = (int)Room.Room;
                        else if (previousX == (int)Room.None && previousZ == (int)Room.None &&
                                nextX == (int)Room.None && nextZ == (int)Room.None)
                            _positions[x, z] = (int)Room.None;
                        else if (previousX <= 1 && nextX <= 1)
                            _positions[x, z] = (int)Room.ZWall;
                        else if (previousZ <= 1 && nextZ <= 1)
                            _positions[x, z] = (int)Room.XWall;
                        else
                            _positions[x, z] = (int)Room.AngleWall;
                    }
                }
            }
        }
    }

    private void Placement()
    {
        for (int x = 0; x < XSide; x++)
        {
            for (int z = 0; z < ZSide; z++)
            {
                if (_positions[x, z] == 0)
                    continue;
                Vector3 position = new Vector3(x * Offset, 0, z * Offset);
                GameObject element = null;

                switch(_positions[x,z])
                {
                    case (int)Room.Room:
                        element = _rooms[Random.Range(0, _rooms.Count - 1)];
                        break;
                    case (int)Room.XWall:
                        element = _xWalls[Random.Range(0, _xWalls.Count - 1)];
                        break;
                    case (int)Room.ZWall:
                        element = _zWalls[Random.Range(0, _zWalls.Count - 1)];
                        break;
                    case (int)Room.AngleWall:
                        element = _angleWalls[Random.Range(0, _angleWalls.Count - 1)];
                        break;
                    default:
                        element = null;
                        break;
                }

                if (x == _startPosition.x && z == _startPosition.y)
                    element = _startRoom;

                if (element != null)
                    Instantiate(element, position, Quaternion.identity);
            }
        }
    }

    public enum Room
    {
        None,
        Room,
        XWall,
        ZWall,
        AngleWall,
        StartRoom = 1
    }
}
