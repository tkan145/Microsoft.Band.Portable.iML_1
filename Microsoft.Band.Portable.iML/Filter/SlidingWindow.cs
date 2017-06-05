using System;
using System.Collections.Generic;

namespace Microsoft.Band.Portable.iML
{
	public class SlidingWindow
	{
		private static IEnumerable<T[,]> GetWindows<T>(T[,] array, int windowWidth, int windowHeight)
		{
			for (var y = 0; y < array.GetLength(1) - windowHeight + 1; y++)
			{
				for (var x = 0; x < array.GetLength(0) - windowWidth + 1; x++)
				{
					var slice = new T[windowWidth, windowHeight];
					CopyArray(array, x, y, slice, 0, 0, windowWidth, windowHeight);
					yield return slice;
				}
			}
		}

		private static void CopyArray<T>(T[,] src, int srcX, int srcY,
										 T[,] dst, int dstX, int dstY,
										 int width, int height)
		{
			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					dst[dstY + x, dstY + y] = src[dstX + x, dstY + y];
				}
			}
		}
	}
}
