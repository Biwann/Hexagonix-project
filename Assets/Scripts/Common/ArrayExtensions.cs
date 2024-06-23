using System;
using System.Collections.Generic;
using System.Linq;

public static class ArrayExtensions
{
    public static T SelectRandom<T>(this IEnumerable<T> collection)
        => SelectRandom(collection.ToArray());

    public static T SelectRandom<T>(this T[] array)
    {
        var r = new Random();

        return array[r.Next(0, array.Length)];
    }

    public static bool IsIdentical(this List<FigureInformation> first, List<FigureInformation> second)
    {
        if (second == null || first.Count != second.Count)
        {
            return false;
        }

        for (int i = 0; i < first.Count; i++)
        {
            if (!first[i].EqualTo(second[i]))
            {
                return false;
            }
        }

        return true;
    }
}