using System;
using System.Collections.Generic;

namespace Utils
{
    public static class IListExtension
    {
        private static readonly Random rng = new((int)DateTime.Now.Ticks & 0x0000FFFF);

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;

                var k = rng.Next(n + 1);

                (list[n], list[k]) = (list[k], list[n]);
            }
        }
    }
}