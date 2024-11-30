using System;

namespace Utils
{
    public static class Dice
    {
        public static int GetValueAt(params int[] diceRiches)
        {
            var sumValue = 0;

            for (int i = 0; i < diceRiches.Length; i++)
            {
                sumValue += RollAt(diceRiches[i]);
            }

            return sumValue;
        }

        public static int RollAt(int diceRiches)
        {
            return new Random().Next(1, diceRiches + 1);
        }
    }
}
