using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public sealed class FiguresManager
{
    public event Action ActivateFigures;
    
    [Inject]
    public void Init(
        GameEvents gameEvents,
        ColumnDestroyer columnDestroyer,
        CellFolder cellFolder)
    {
        _gameEvents = gameEvents;
        _cellFolder = cellFolder;
        _columnDestroyer = columnDestroyer;

        _columnDestroyer.ColumnChecked += OnColumnChecked;
        GameEvents.LevelChanging += OnRestarting;
    }

    private void OnRestarting()
    {
        _columnDestroyer.ColumnChecked -= OnColumnChecked;
        GameEvents.LevelChanging -= OnRestarting;
        _figureCreators.Clear();
    }

    public void AddFigure(FigureCreator figureCreator)
    {
        _figureCreators.Add(figureCreator);
    }

    private void OnColumnChecked()
    {
        ActivateFiguresIfAllEmpty();
        ForbidFigureIfCantPlace();
        CheckLooseCondition();
    }

    private void ActivateFiguresIfAllEmpty()
    {
        if (_figureCreators.All(f => !f.HasFigure))
        {
            ActivateFigures?.Invoke();
        }
    }

    private void ForbidFigureIfCantPlace()
    {
        foreach (var creator in _figureCreators)
        {
            if (!creator.HasFigure)
            {
                creator.SetAllowPlaceFigure(true);
                continue;
            }
            
            var figure = creator.FigureInformation;
            creator.SetAllowPlaceFigure(CanPlaceFigure(figure));
        }
    }

    private void CheckLooseCondition()
    {
        if (_figureCreators.All(f => !f.HasFigure || !f.CanPlaceFigure()))
        {
            _gameEvents.InvokeNoMovesLeft();
        }
    }

    private bool CanPlaceFigure(FigureInformation figure)
    {
        foreach (var cell in _cellFolder.GetCells())
        {
            var canPlace = false;

            foreach (var figurePart in figure.Parts)
            {
                var canPlacePositionX = cell.Position.X + figurePart.Position.X;
                var canPlacePositionY = cell.Position.Y + figurePart.Position.Y;

                // shift in position coordinates because of difference in Y direction
                if (cell.Position.Y % 2 != 0 && figurePart.Position.Y % 2 != 0)
                {
                    canPlacePositionX++;
                }

                var cellToCheck = _cellFolder.GetCellByPosition(canPlacePositionX, canPlacePositionY);
                if (cellToCheck == null || !cellToCheck.IsEmpty)
                {
                    canPlace = false;
                    break;
                }

                canPlace = true;
            }

            if (canPlace)
            {
                return true;
            }
        }

        return false;
    }

    private List<FigureCreator> _figureCreators = new();
    private GameEvents _gameEvents;
    private CellFolder _cellFolder;
    private ColumnDestroyer _columnDestroyer;
}