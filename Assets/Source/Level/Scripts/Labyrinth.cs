using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth : MonoBehaviour
{
    [SerializeField] private int _sideX;
    [SerializeField] private int _sideY;
    [SerializeField] private int _sideZ;
    [SerializeField] private List<Vector2> _voidPositions;
    [SerializeField] private int _amountRandomVoidPositions;
    [SerializeField] private GameObject[] _walls;
    [SerializeField] private Vector3 _offset;

    private int[,] _array;

    private void Awake()
    {
        _array = new int[_sideX, _sideZ];
        Generate();
        Build();
    }

    private void Generate()
    {
        for (int x = 0; x < _sideX; x++)
            for (int z = 0; z < _sideZ; z++)
                _array[x, z] = Random.Range(0, _sideY);

        for (int i = 1; i < _voidPositions.Count; i += 2)
        {
            _voidPositions[i - 1] = CheckPosition(_voidPositions[i - 1]);
            _voidPositions[i] = CheckPosition(_voidPositions[i]);
            GeneratePath(_voidPositions[i-1], _voidPositions[i]);
        }

        for(int i = 0; i<_amountRandomVoidPositions; i++)
        {
            Vector2 newVoidPosition = new Vector2(Random.Range(0, _sideX), (Random.Range(0, _sideZ)));
            GeneratePath(newVoidPosition, _voidPositions[_voidPositions.Count-1]);
            _voidPositions.Add(newVoidPosition);
        }
    }

    private Vector2 CheckPosition(Vector2 position)
    {
        if (position.x < 0) position.x = 0;
        if (position.y < 0) position.y = 0;
        if (position.x > _sideX - 1) position.x = _sideX - 1;
        if (position.y > _sideZ - 1) position.y = _sideZ - 1;
        return position;
    }

    private Vector2 GeneratePath(Vector2 from, Vector2 to)
    {
        if ((from.x == to.x && System.Math.Abs(from.y - to.y) <= 1) ||
            (System.Math.Abs(from.x - to.x) <= 1 && (from.y == to.y)))
        {
            _array[(int)from.x, (int)from.y] = 0;
            _array[(int)to.x, (int)to.y] = 0;
            return new Vector2(to.x, to.y);
        }

        int newToX = (int)Random.Range(System.Math.Min(from.x, to.x), System.Math.Max(from.x, to.x) + 1);
        int newToZ = (int)Random.Range(System.Math.Min(from.y, to.y), System.Math.Max(from.y, to.y) + 1);
        Vector2 newTo = new Vector2(newToX, newToZ);

        return GeneratePath(GeneratePath(from, newTo), to);
    }

    private void Build()
    {
        for (int x = 0; x < _sideX; x++)
        {
            for (int z = 0; z < _sideZ; z++)
            {
                for (int y = 0; y < _array[x, z]; y++)
                {
                    Vector3 nextPosition = new Vector3(
                        transform.position.x + _offset.x * x,
                        transform.position.y + _offset.y * y,
                        transform.position.z + _offset.z * z);
                    Instantiate(_walls[Random.Range(0, _walls.Length)], nextPosition, transform.rotation, transform);
                }
            }
        }
    }
}
