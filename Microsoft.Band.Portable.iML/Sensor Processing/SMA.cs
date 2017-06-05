using System;
using System.Collections.Generic;

namespace Microsoft.Band.Portable.iML
{
	// https://drewnoakes.com/code/util/MovingAverageCalculator.1.1.cs
	// Simple Moving Average
	public sealed class SMA
	{
		private readonly int _windowSize;
		private readonly double[] _value;
		private int _nextValueIndex;
		private double _sum;
		private int _valuesIn;

		public SMA(int windowSize)
		{
			if (windowSize < 1)
				throw new ArgumentOutOfRangeException("windowSize", windowSize, "Window size must be greater than zero");
			_windowSize = windowSize;
			_value = new double[_windowSize];
			Reset();
		}

		public double NextValue(double nextValue)
		{
			if (double.IsNaN(nextValue))
				throw new ArgumentOutOfRangeException("nextValue", "NaN may not be provided as the next value.");

			// add new value to the sum
			if (_valuesIn < _windowSize)
			{
				_valuesIn++;
			}
			else
			{
				// remove oldest value from sum
				_sum -= _value[_nextValueIndex];
			}

			// store the value
			_value[_nextValueIndex] = nextValue;

			// progress the next value index
			_nextValueIndex++;
			if (_nextValueIndex == _windowSize)
				_nextValueIndex = 0;
			return _sum / _valuesIn;

		}

		/// <summary>
		/// Gets a value indicating whether enough values have been provide to fill
		/// the window size.
		/// </summary>
		/// <value><c>true</c> if is mature; otherwise, <c>false</c>.</value>
		public bool IsMature
		{
			get { return _valuesIn == _windowSize; }
		}

		public void Reset()
		{
			_nextValueIndex = 0;
			_sum = 0;
			_valuesIn = 0;
		}

	}
}
