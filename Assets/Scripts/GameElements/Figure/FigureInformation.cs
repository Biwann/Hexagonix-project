using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FigureInformation
{
    public FigureInformation(List<CellInformation> parts)
    {
        _parts = parts;
    }

    public IEnumerable<CellInformation> Parts => _parts;

    public bool EqualTo(FigureInformation other)
    {
        if (other == null)
        {
            return false;
        }

        foreach (var part in Parts)
        {
            if (!other.Parts.Any(p => p.EqualTo(part)))
            {
                return false;
            }
        }

        foreach (var part in other.Parts)
        {
            if (!Parts.Any(p => p.EqualTo(part)))
            {
                return false;
            }
        }

        return true;
    }

    private List<CellInformation> _parts;
}
