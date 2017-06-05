using System;
namespace Microsoft.Band.Portable.iML
{
	public class ExponentialMVA
	{
		private double alpha;
		private Double oldValue;
		public ExponentialMVA(double alpha)
		{
			this.alpha = alpha;
		}

		public double average(double value)
		{
			if (oldValue.Equals(null))
			{
				oldValue = value;
				return value;
			}
			double newValue = oldValue + alpha * (value - oldValue);
			oldValue = newValue;
			return newValue;
		}
	}
}
