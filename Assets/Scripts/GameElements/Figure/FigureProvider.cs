using System.Collections.Generic;
using System.Linq;

public class FigureProvider
{
    public FigureProvider(FiguresCollection figuresCollection)
    {
        _figureCollection = figuresCollection;
    }

    public FigureInformation GetNextFigure()
    {
        var figure = _figureCollection.GetAllFigures()
            .Where(newFigure => _lastUsedFigures.All(usedFigure => !newFigure.EqualTo(usedFigure)))
            .SelectRandom();

        _lastUsedFigures.Enqueue(figure);

        var figureAmount = _figureCollection.GetAllFigures().Count();
        var maxUsedFigures = MAX_LAST_USED_FIGURES < figureAmount
            ? MAX_LAST_USED_FIGURES
            : figureAmount - 1;
        if (_lastUsedFigures.Count > maxUsedFigures)
        {
            _lastUsedFigures.Dequeue();
        }

        return figure;
    }

    private const int MAX_LAST_USED_FIGURES = 7;
    private readonly FiguresCollection _figureCollection;
    private readonly Queue<FigureInformation> _lastUsedFigures = new();
}