using System.Drawing;
using UnityEngine;
using Zenject;

public class GameFieldGenerator : MonoBehaviour
{
    [Inject]
    public void Inject(
        Tracer tracer, 
        CellFolder cellFolder,
        HexagonixFieldProvider fieldProvider,
        PrefabLoader prefabLoader   )
    {
        _tracer = tracer;
        _cellFolder = cellFolder;
        _fieldProvider = fieldProvider;

        _cellPrefab = prefabLoader.Cell;

        GenerateField();
    }


    [ContextMenu("Clear Cells")]
    private void ClearChildren()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private void GenerateField()
    {
        _tracer?.TraceDebug("Starting generate field");
        ClearChildren();
        InstanciateCells();
        _tracer?.TraceDebug("Field Generating ended");
    }

    [ContextMenu("Intanciate Cells")]
    private void InstanciateCells()
    {
        int _size = _fieldProvider.Radius * 2;

        for (int y = -_size / 2; y <= _size / 2; y++)
        {
            for (int x = -_size / 2; x <= _size / 2; x++)
            {
                if (!_fieldProvider.IsCoordinateOnField(x, y))
                {
                    continue;
                }

                var position = HexagonPositionConverter.GetRealPosition(Vector3.zero, x, y);
                var gameObject = Instantiate(_cellPrefab, position, Quaternion.identity);

                gameObject.name = $"cell ({x}, {y})";
                gameObject.transform.parent = transform;

                var cell = gameObject.GetComponent<Cell>();
                cell.Init(new CellInformation(new Point(x, y)));
                _cellFolder?.AddCell(cell);
            }
        }
    }

    private Tracer _tracer;
    private CellFolder _cellFolder;
    private HexagonixFieldProvider _fieldProvider;
    private GameObject _cellPrefab;
}
