using System.Drawing;
using UnityEngine;
using Zenject;

public sealed class Cell : MonoBehaviour
{
    [Inject]
    public void Inject(Tracer tracer)
    {
        _tracer = tracer;
    }

    public CellInformation CellInformation { get; private set; }

    public void Init(CellInformation cellInformation)
    {
        CellInformation = cellInformation;
    }

    public void Place(IPlacebleObject item)
    {
        if (CellInformation.TryPlaceItem(item))
        {
            _tracer.TraceDebug($"{item} placed in {CellInformation.Position}");
        }
        else
        {
            _tracer.TraceWarning($"{item} cant be placed in {CellInformation.Position}");
        }

    }

    public Point Position => CellInformation.Position;

    public bool IsEmpty => CellInformation.IsEmpty;
    
    private Tracer _tracer;
}
