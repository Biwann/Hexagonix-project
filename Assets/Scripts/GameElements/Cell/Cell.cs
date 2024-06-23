using System;
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

    public bool TryPlace(IPlacebleObject item, Action<Vector3> onSuccess = null)
    {
        if (!CellInformation.TryPlaceItem(item))
        {
            _tracer?.TraceWarning($"{item} cant be placed in {CellInformation.Position}");
            return false;
        }
        
        onSuccess?.Invoke(transform.position);
        _tracer?.TraceDebug($"{item} placed in {CellInformation.Position}");

        return true;
    }

    public Point Position => CellInformation.Position;

    public bool IsEmpty => CellInformation.IsEmpty;
    
    private Tracer _tracer;
}
