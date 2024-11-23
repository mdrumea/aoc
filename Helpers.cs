using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
	internal static class Helpers
	{
		public static T[,] ToMatrix<T>(this string[] lines)
		{
			T[,] grid = new T[lines.Length, lines[0].Length];

			for (int i = 0; i < lines.Length; i++)
			{
				for (int j = 0; j < lines[0].Length; j++)
				{
					if (typeof(T) == typeof(int))
					{
						grid[i, j] = (T)(object)int.Parse(lines[i][j].ToString());
					}
					else if (typeof(T) == typeof(string))
					{
						grid[i, j] = (T)(object)lines[i][j].ToString();
					}
					else
					{
						throw new NotImplementedException();
					}

				}
			}

			return grid;
		}

		public static int RowCount<T>(this T[,] array) => array.GetLength(0);

		public static int ColumnCount<T>(this T[,] array) => array.GetLength(1);

		public static T[] GetRow<T>(this T[,] array, int row)
		{
			T[] result = new T[array.ColumnCount()];

			for (int i = 0; i < array.ColumnCount(); i++)
			{
				result[i] = array[row, i];
			}

			return result;
		}

		public static T[] GetColumn<T>(this T[,] array, int column)
		{
			T[] result = new T[array.RowCount()];

			for (int i = 0; i < array.RowCount(); i++)
			{
				result[i] = array[i, column];
			}

			return result;
		}
	}
}
