
using Unity.VisualScripting;
using UnityEngine;

public class GameFieldGenerator : MonoBehaviour
{
    const float PREFAB_HEIGHT = 1.048f;
    const float PREFAB_WIDTH = 0.9f;

    [SerializeField] private int _radius;
    [SerializeField] private float _offset;
    [SerializeField] private GameObject _cellPrefab;

    void Start()
    {
        GenerateField();
    }

    [ContextMenu("Clear")]
    private void ClearChildren()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    [ContextMenu("Generate")]
    private void GenerateField()
    {
        ClearChildren();
        int _size = _radius * 2;

        for (int y = -_size / 2; y <= _size / 2; y++)
        {
            for (int x = -_size / 2; x <= _size / 2; x++)
            {
                if (!IsCoordinateOnField(x, y))
                {
                    continue;
                }

                float realX = x * (PREFAB_WIDTH + _offset) + (y % 2 == 0 ? 0 : PREFAB_WIDTH / 2);
                float realY = y * (PREFAB_HEIGHT * 3 / 4 + _offset);
                var position = new Vector3(realX, realY, 0);
                var cell = Instantiate(_cellPrefab, position, Quaternion.identity);

                cell.name = $"cell ({x}, {y})";
                cell.transform.parent = transform;
            }
        }
    }
    public bool IsCoordinateOnField(int x, int y)
    {
        if (y % 2 == 0)
        {
            if (Mathf.Abs(x) > _radius - Mathf.Abs(y) / 2)
            {
                return false;
            }
        }
        else
        {
            if (x < -(_radius - Mathf.Abs(y) / 2) || x >= _radius - Mathf.Abs(y) / 2)
            {
                return false;
            }
        }

        return true;
    }
}
