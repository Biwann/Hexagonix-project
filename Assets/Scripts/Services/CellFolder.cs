using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public sealed class CellFolder
{
    public CellFolder() { }

    public Cell GetCellByPosition(Point position) =>
        cells.FirstOrDefault(c => c.Position == position);

    public Cell GetCellByPosition(int x, int y) =>
        GetCellByPosition(new Point(x, y));

    public bool CellExists(Point position) =>
        cells.Any(c => c.Position == position);
    public bool CellExists(int x, int y) =>
        CellExists(new Point(x, y));

    public void AddCell(Cell cell)
    {
        var position = cell.Position;
        if (CellExists(position))
        {
            throw new Exception($"cell {position} already exists");
        }

        cells.Add(cell);
    }

    public IEnumerable<Cell> GetCells() => cells;

    private List<Cell> cells = new();
}
