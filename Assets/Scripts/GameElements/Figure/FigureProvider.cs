using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Video;

public class FigureProvider
{
    public FigureProvider(FiguresCollection figuresCollection)
    {
        _figureCollection = figuresCollection;
    }

    public FigureInformation GetNextFigure()
    {
        var figure = _figureCollection.GetAllFigures()
            .Where(newFigure => _nearlyUsedFigures.All(usedFigure => !newFigure.EqualTo(usedFigure)))
            .SelectRandom();

        _nearlyUsedFigures.Enqueue(figure);
        if (_nearlyUsedFigures.Count > MAX_USED_FIGURES)
        {
            _nearlyUsedFigures.Dequeue();
        }

        return figure;
    }

    private const int MAX_USED_FIGURES = 3;
    private readonly FiguresCollection _figureCollection;
    private readonly Queue<FigureInformation> _nearlyUsedFigures = new();
}