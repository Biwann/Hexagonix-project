using System;
using System.Collections.Generic;
using System.Drawing;
using Zenject;

public sealed class ColumnDestroyer
{
    public event Action ColumnChecked;

    [Inject]
    public void Inject(
        CellFolder cellFolder,
        HexagonixFieldProvider fieldProvider,
        Tracer tracer,
        ScoresOnLevel scores)
    {
        _cellFolder = cellFolder;
        _fieldProvider = fieldProvider;
        _tracer = tracer;
        _scores = scores;

        GameEvents.FigurePlaced += OnPlacedOnField;
    }

    private void OnPlacedOnField()
    {
        DestroyFilledColumns();
        ColumnChecked?.Invoke();
    }

    private void DestroyFilledColumns()
    {
        var radius = _fieldProvider.Radius;
        var cellsToClear = new List<Cell>();
        var multiplier = 0;
        var pointsToAdd = 0;

        multiplier += MarkForDestroyHorizontal(cellsToClear, radius);
        multiplier += MarkForDestroyVertical(cellsToClear, radius, isFromLeftToRight: true);
        multiplier += MarkForDestroyVertical(cellsToClear, radius, isFromLeftToRight: false);

        if (cellsToClear.Count > 0)
        {
            _tracer.TraceDebug($"clearing {cellsToClear.Count}");

            cellsToClear.ForEach(c =>
            {
                pointsToAdd += c.ClearCellAndGetPoints();
            });
        }

        if (multiplier > 0)
        {
            var resultedPoints = pointsToAdd * multiplier;
            _tracer.TraceDebug($"points {pointsToAdd} * multiplier {multiplier} = points to add {resultedPoints}" );
            _scores.AddScore(resultedPoints);
        }
    }

    private int MarkForDestroyHorizontal(List<Cell> folder, int radius)
    {
        var multiplier = 0;
        for (int y = radius; y >= -radius; y--)
        {
            var tempFolder = new List<Cell>();
            var isFindingFirstCell = true;

            for (int x = -radius - 1; x <= radius + 1; x++)
            {
                // find first coordinate where cell exists
                if (isFindingFirstCell)
                {
                    if (_cellFolder.CellExists(x, y))
                    {
                        isFindingFirstCell = false;
                    }
                    else
                    {
                        continue;
                    }
                }

                // when all in line is not empty and we went to end
                if (!_cellFolder.CellExists(x, y))
                {
                    folder.AddRange(tempFolder);
                    multiplier++;
                    break;
                }

                var cell = _cellFolder.GetCellByPosition(x, y);

                if (!cell.IsEmpty)
                {
                    tempFolder.Add(cell);
                }
                // if not all is filled in line
                else
                {
                    tempFolder.Clear();
                    break;
                }
            }
        }

        return multiplier;
    }

    private int MarkForDestroyVertical(List<Cell> folder, int radius, bool isFromLeftToRight)
    {
        var multiplier = 0;
        var cellsToCheck = new List<Cell>();

        // add all top cells
        for (int x = -radius / 2; x <= radius / 2; x++)
        {
            if (!_cellFolder.CellExists(x, radius))
            {
                _tracer.TraceWarning($"cell {x}, {radius} doesnt exists");
                continue;
            }
            var cell = _cellFolder.GetCellByPosition(x, radius);
            cellsToCheck.Add(cell);
        }

        // add side cells
        {
            var sign = isFromLeftToRight ? -1 : 1;
            var cellPosition = new Point(sign * -radius / 2, radius);
            while (_cellFolder.CellExists(cellPosition))
            {
                var cell = _cellFolder.GetCellByPosition(cellPosition);
                cellsToCheck.Add(cell);

                if (isFromLeftToRight)
                {
                    cellPosition = MoveRightToLeftDown(cellPosition);
                }
                else
                {
                    cellPosition = MoveLeftToRightDown(cellPosition);
                }
            }
        }

        // check all cells in diagonal
        foreach (var startCell in cellsToCheck)
        {
            var position = startCell.Position;
            var tempFolder = new List<Cell>();
            while (_cellFolder.CellExists(position))
            {
                var cell = _cellFolder.GetCellByPosition(position);
                // if not all in line filled
                if (cell.IsEmpty)
                {
                    tempFolder.Clear();
                    break;
                }

                tempFolder.Add(cell);

                if (!isFromLeftToRight)
                {
                    position = MoveRightToLeftDown(position);
                }
                else
                {
                    position = MoveLeftToRightDown(position);
                }
            }

            if (tempFolder.Count > 0)
            {
                multiplier++;
                folder.AddRange(tempFolder);
            }
        }

        return multiplier;
    }

    private Point MoveLeftToRightDown(Point position)
    {
        var deltaX = position.Y % 2 == 0 ? 1 : 0;
        return new Point(position.X - deltaX, position.Y - 1);
    }

    private Point MoveRightToLeftDown(Point position)
    {
        var deltaX = position.Y % 2 == 0 ? 0 : 1;
        return new Point(position.X + deltaX, position.Y - 1);
    }

    private CellFolder _cellFolder;
    private HexagonixFieldProvider _fieldProvider;
    private Tracer _tracer;
    private ScoresOnLevel _scores;
}