using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Extensions
    {
        public static bool TryGetValue<T>(this T[,] values, int x, int y, out T value)
        {
            if (IsValid(values, x, y))
            {
                value = values[x, y];
                return true;
            }

            value = default;
            return false;
        }

        public static bool TryGetValueOfType<Torigin, Tfound>(this Torigin[,] values, int x, int y, out Tfound value) where Tfound : class, Torigin
        {
            if (IsValid(values, x, y))
            {
                value = values[x, y] as Tfound;

                if(value == null) return false;

                return true;
            }

            value = default;
            return false;
        }

        public static bool IsValid<T>(this T[,] values, int x, int y)
        {
            return x >= 0 && x < values.GetLength(0) && y >= 0 && y < values.GetLength(1);
        }
    }
}