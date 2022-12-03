using System.Collections.Generic;
using System.Linq;

namespace Tetris.Game.Pieces
{
    public static class ArrayExtensions
    {
        public static T[] GetRow<T>(this T[,] arr, int dimension)
        {
            List<T> row = new List<T>();

            for (int i = 0; i < arr.GetLength(1); i++)
                row.Add(arr[dimension, i]);

            return row.ToArray();
        }

        public static T[] ToLinear<T>(this T[,] arr)
        {
            IEnumerable<T> result = Enumerable.Empty<T>();

            for (int i = 0; i < arr.Rank; i++)
                result = result.Concat(arr.GetRow(i));

            return result.ToArray();
        }
    }
}
