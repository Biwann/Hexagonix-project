using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public sealed class FiguresManager
{
    public event Action ActivateFigures;
    
    [Inject]
    public void Init(GameEvents gameEvents)
    {
        gameEvents.PlacedOnField += OnFigurePlaced;
    }

    public void AddFigure(FigureCreator figureCreator)
    {
        FigureCreators.Add(figureCreator);
    }

    public List<FigureCreator> FigureCreators { get; private set; } = new();

    private void OnFigurePlaced()
    {
        if (FigureCreators.All(f => !f.HasFigure))
        {
            ActivateFigures?.Invoke();
        }
    }
}