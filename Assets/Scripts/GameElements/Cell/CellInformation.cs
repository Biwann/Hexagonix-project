using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CellInformation
{
    public CellInformation(Point position)
    {
        Position = position;
    }

    public Point Position { get; private set; }

    public bool TryPlaceItem(IPlacebleObject item)
    {
        if (!IsEmpty)
        {
            return false;
        }

        Item = item;
        return true;
    }

    public IPlacebleObject Item
    {
        get 
        {
            return _item; 
        }
        private set
        {
            if (value != _item)
            {
                _item = value;
            }
        }
    }

    public bool IsEmpty => _item == null;

    public bool EqualTo(CellInformation otherCell)
        => otherCell != null && otherCell.Position.Equals(Position);

    private IPlacebleObject _item;
}
