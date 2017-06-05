using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Band.Portable.Sensors;

namespace Microsoft.Band.Portable.iML
{
	public class SensorViewModel<T> : BaseBandViewModel
	where T : IBandSensorReading
	{
		private int lines;
		private ObservableRangeCollection<BaseBandViewModel> sensors;

		public SensorViewModel(string type, BandSensorBase<T> sensor, ObservableRangeCollection<BaseBandViewModel> sensors, int lines)
		{
			this.lines = lines;
			this.sensors = sensors;
			this.sensor = sensor;
			this.reading = null;
			this.isSensorEnabled = false;

			Type = type;


		}

		private async Task ApplySensorState()
		{
			if (isSensorEnabled)
			{
				var userConsenting = Sensor as IUserConsentingBandSensor<T>;
				if (userConsenting == null
				   || userConsenting.UserConsented == UserConsent.Granted
				   || await userConsenting.RequestUserConsent())
				{
					try
					{
						await Sensor.StartReadingsAsync();

					}
					catch (Exception ex)
					{
						Debug.WriteLine("Problem starting sensor: " + ex);
						IsSensorEnabled = false;
					}
				}
				else
				{
					IsSensorEnabled = false;
				}
			}
		}

		#region Properties
		private BandSensorBase<T> sensor;
		public BandSensorBase<T> Sensor { get { return sensor; } }

		public string Type { get; private set; }

		private IBandSensorReading reading;
		public IBandSensorReading Reading
		{
			get { return reading; }
			set
			{
				if (reading != value)
				{
					//ReadingString = GetStringValue(value);
					RaisePropertyChanged("Reading");
				}
			}
		}

		private string readingString;
		public string ReadingString
		{
			get { return readingString; }
			set { Set(ref readingString, value); }
		}

		private bool isSensorEnabled;
		public bool IsSensorEnabled
		{
			get { return isSensorEnabled; }
			set
			{
				if (isSensorEnabled != value)
				{
					isSensorEnabled = value;
					RaisePropertyChanged("IsSensorEnabled");
					ApplySensorState();
				}
			}
		}
		#endregion
	}
}
