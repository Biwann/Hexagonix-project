using System.Drawing;
using UnityEngine;
using Zenject;

public class GameFieldGenerator : MonoBehaviour
{
    [Inject]
    public void Inject(
        Tracer tracer,
        CellFolder cellFolder)
    {
        _tracer = tracer;
        _cellFolder = cellFolder;
    }

    [SerializeField] private int _radius;
    [SerializeField] private GameObject _cellPrefab;

    void Start()
    {
        GenerateField();
    }

    private void ClearChildren()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private void GenerateField()
    {
        _tracer.TraceDebug("Starting generate field");
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

                var position = HexagonPositionConverter.GetRealPosition(Vector3.zero, x, y);
                var gameObject = Instantiate(_cellPrefab, position, Quaternion.identity);

                gameObject.name = $"cell ({x}, {y})";
                gameObject.transform.parent = transform;
                
                var cell = gameObject.GetComponent<Cell>();
                cell.Init(new CellInformation(new Point(x, y)));
                _cellFolder.AddCell(cell);
            }
        }
        _tracer.TraceDebug("Field Generating ended");
    }
    public bool IsCoordinateOnField(int x, int y)
    {
        return !(
            y % 2 == 0
            ? Mathf.Abs(x) > _radius - Mathf.Abs(y) / 2
            : x < -(_radius - Mathf.Abs(y) / 2) || x >= _radius - Mathf.Abs(y) / 2);
    }

    private Tracer _tracer;
    private CellFolder _cellFolder;
}