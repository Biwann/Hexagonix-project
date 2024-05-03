using System;
using UnityEngine;

public sealed class Tracer
{
    public Tracer(Action<string> traceDebug, Action<string> traceWarning)
    {
        if (traceDebug == null) throw new ArgumentNullException();
        if (traceWarning == null) throw new ArgumentNullException();

        _traceDebug = traceDebug;
        _traceWarning = traceWarning;
    }

    public bool CanTraceDebug = true;

    public bool CanTraceWarning = true;

    public void TraceDebug(string message)
    {
        if (CanTraceDebug)
            _traceDebug(message);
    }

    public void TraceWarning(string message)
    {
        if (CanTraceWarning)
            _traceWarning(message);
    }

    private Action<string> _traceDebug;
    private Action<string> _traceWarning;

}