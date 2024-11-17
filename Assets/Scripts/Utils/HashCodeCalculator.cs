using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public static class HashCodeCalculator
    {
        public static string CalculateHashCode(int[][] grid)
        {
            var tmpSource = grid.SelectMany(x => GetByteArrayFromIntArray(x)).ToArray();

            var tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            return ByteArrayToString(tmpHash);

        }

        private static byte[] GetByteArrayFromIntArray(int[] intArray)
        {
            var data = new byte[intArray.Length * 4];

            for (int i = 0; i < intArray.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes(intArray[i]), 0, data, i * 4, 4);
            }

            return data;
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            int i;

            var sOutput = new StringBuilder(arrInput.Length);

            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }

            return sOutput.ToString();
        }
    }
}