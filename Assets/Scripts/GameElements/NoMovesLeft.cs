using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class NoMovesLeft
{
    public NoMovesLeft(
        CellFolder cellFolder,
        GameEvents gameEvents,
        UnityObjectLifeController unityController,
        Tracer tracer)
    {
        _cellFolder = cellFolder;
        _gameEvents = gameEvents;
        _unityController = unityController;
        _tracer = tracer;

        Subscribe();
        GameEvents.LevelChanging += Unsubscribe;
    }

    private void Subscribe()
    {
        GameEvents.NoMovesLeft += ChangePlacedFiguresColor;
    }
    private void Unsubscribe()
    {
        GameEvents.NoMovesLeft -= ChangePlacedFiguresColor;
    }

    private void ChangePlacedFiguresColor()
    {
        _unityController.StartCoroutine(ChangePlacedFiguresColorCoroutine());
    }

    private System.Collections.IEnumerator ChangePlacedFiguresColorCoroutine()
    {
        var filledCells = _cellFolder.GetCells().Where(c => !c.IsEmpty).ToArray();
        var tweenerList = new List<Tweener>();
        _tracer.TraceDebug($"filled cells: {filledCells.Length}");
        var a = 0;

        foreach (var cell in filledCells)
        {
            var figure = cell.GetPlacedGameObject();

            var delay = Random.Range(0.25f, 1f);
            var material = figure.GetComponentInChildren<SpriteRenderer>().material;

            var tweener = AnimateMaterial(material, delay);
            tweenerList.Add(tweener);

            _tracer.TraceDebug($"start animation {++a}");
        }

        a = 0;
        foreach (var twnr in tweenerList)
        {
            _tracer.TraceDebug($"waiting animation: {++a}");
            if (twnr.IsActive())
                yield return twnr.WaitForCompletion();
        }

        _gameEvents.InvokeGameEnd();
    }

    private Tweener AnimateMaterial(Material material, float delay)
        => material.DOColor(Color.gray, 0.5f).SetDelay(delay);

    private readonly CellFolder _cellFolder;
    private readonly GameEvents _gameEvents;
    private readonly UnityObjectLifeController _unityController;
    private readonly Tracer _tracer;
}