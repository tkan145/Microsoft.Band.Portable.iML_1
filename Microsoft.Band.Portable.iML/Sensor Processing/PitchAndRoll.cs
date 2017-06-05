using System;
using Accord.Math;

namespace Microsoft.Band.Portable.iML
{

	/// <summary>
	/// Pitch and roll.
	/// rollAngle = arctang(-Gx / Gz)
	/// pitchAngle = arctan(Gy/ Sqrt(Gx*Gx + Gz* Gz))
	/// 
	/// The unit of pitch and roll angle calculated in degree
	/// </summary>
	public class PitchAndRoll
	{
		public PitchAndRoll(double x, double y, double z)
		{
			double pitch = 180 * Math.Atan(x / Math.Sqrt(Math.Pow(y, 2) + Math.Pow(z, 2))) / Math.PI;
			double roll = 180 * Math.Atan(y / Math.Sqrt(Math.Pow(z, 2) + Math.Pow(z, 2))) / Math.PI;
			double yaw = 180 * Math.Atan(z / Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2))) / Math.PI;

			var mag = Math.Sqrt(Math.Pow(x * x + y * y + z * z, 2));        // Magnitude
		}

		public void AutoCalibrate(double xRaw, double yRaw, double zRaw)
		{   //https://learn.adafruit.com/adafruit-analog-accelerometer-breakouts/programming
			//http://www.st.com/content/ccc/resource/technical/document/application_note/a0/f0/a0/62/3b/69/47/66/DM00119044.pdf/files/DM00119044.pdf/jcr:content/translations/en.DM00119044.pdf

		}

		int xRawMin = 512;
		int xRawMax = 512;

		int yRawMin = 512;
		int yRawMax = 512;

		int zRawMin = 512;
		int zRawMax = 512;
	}
}
