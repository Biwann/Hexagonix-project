using System.Collections.Generic;
using System.Text;

public static class NumberToSpritesConverter
{
    public static string Convert(int num)
    {
        var strBuilder = new StringBuilder();
        var stack = new Stack<int>();

        if (num == 0)
        {
            stack.Push(num);
        }

        for (;num > 0;) 
        { 
            stack.Push(num%10);
            num /= 10;
        }

        while (stack.Count > 0)
        {
            strBuilder.Append($"<sprite={stack.Pop()}>");
        }

        return strBuilder.ToString();
    }
}
