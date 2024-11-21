using System;

namespace Utils
{
    public static class Roll
    {
        public static int GetValue(int diceRiches)
        {
            return new Random().Next(1, diceRiches);
        }
    }
}
