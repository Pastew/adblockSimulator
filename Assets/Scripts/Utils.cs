using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Utils
{
    // Source: https://stackoverflow.com/questions/33643104/shuffling-a-stackt
    public static void Shuffle<T>(this Queue<T> queue)
    {
        var values = queue.ToArray();
        queue.Clear();
        Random rnd = new Random();
        foreach (var value in values.OrderBy(x => rnd.Next()))
            queue.Enqueue(value);
    }
}
