using System.Collections.Generic;
using System.Drawing;

public sealed class FiguresCollection
{
    public FiguresCollection()
    {
        // 2 cells
        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(-1, 0)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(-1, 1)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, 1)),
        }));

        // 3 cells
        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, 1)),
            new CellInformation(new Point(-1, 1)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, 1)),
            new CellInformation(new Point(-1, 0)),
        }));

        // 4 cells
        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, 1)),
            new CellInformation(new Point(-1, 1)),
            new CellInformation(new Point(0, 2)),
        }));

        // 5 cells
        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(1, 0)),
            new CellInformation(new Point(2, 0)),
            new CellInformation(new Point(-1, 1)),
            new CellInformation(new Point(-1, 2)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(1, 0)),
            new CellInformation(new Point(2, 0)),
            new CellInformation(new Point(1, 1)),
            new CellInformation(new Point(1, 2)),
        }));



        
    }

    public IEnumerable<FigureInformation> GetAllFigures() => _figures;

    private List<FigureInformation> _figures = new();
}
