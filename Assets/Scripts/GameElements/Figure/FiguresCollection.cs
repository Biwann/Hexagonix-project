using System.Collections.Generic;
using System.Drawing;

public sealed class FiguresCollection
{
    public FiguresCollection()
    {
        // doubles neccesery to make figures appear more often

        InitFiguresWith1Cell();
        InitFiguresWith1Cell();

        InitFiguresWith2Cells();
        InitFiguresWith2Cells();

        InitFiguresWith3Cells();
        InitFiguresWith3Cells();

        InitFiguresWith4Cells();
        InitFiguresWith4Cells();

        InitFiguresWith5Cells();
    }

    public IEnumerable<FigureInformation> GetAllFigures() => _figures;

    private void InitFiguresWith1Cell()
    {
        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0))
        }));
    }

    private void InitFiguresWith2Cells()
    {
        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(1, 0)),
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
    }

    private void InitFiguresWith3Cells()
    {
        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(-1, 1)),
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(1, 0)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(-1, 0)),
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, 1)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(-1, -1)),
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(1, 0)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 1)),
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, -1)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(-1, 1)),
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(-1, -1)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(-1, 1)),
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, -1)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(-1, -1)),
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, 1)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(-1, 0)),
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, 1)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(-1, -1)),
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, 1)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(-1, -1)),
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, -1)),
        }));

    }

    private void InitFiguresWith4Cells()
    {
        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(0, 1)),
            new CellInformation(new Point(-1, 1)),
            new CellInformation(new Point(0, 2)),
        }));
    }

    private void InitFiguresWith5Cells()
    {
        /* angle figure
        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(1, 0)),
            new CellInformation(new Point(2, 0)),
            new CellInformation(new Point(1, 1)),
            new CellInformation(new Point(1, 2)),
        }));
        */

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(1, 0)),
            new CellInformation(new Point(0, 1)),
            new CellInformation(new Point(0, 2)),
            new CellInformation(new Point(1, 2)),
        }));

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
            new CellInformation(new Point(-1, -1)),
            new CellInformation(new Point(-1, -2)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(1, 0)),
            new CellInformation(new Point(2, 0)),
            new CellInformation(new Point(2, 1)),
            new CellInformation(new Point(3, 2)),
        }));

        _figures.Add(new FigureInformation(new List<CellInformation>
        {
            new CellInformation(new Point(0, 0)),
            new CellInformation(new Point(1, 0)),
            new CellInformation(new Point(2, 0)),
            new CellInformation(new Point(2, -1)),
            new CellInformation(new Point(3, -2)),
        }));
    }

    private List<FigureInformation> _figures = new();
}
