using System;
using System.Threading.Tasks;
using Microsoft.Band.Portable.Sensors;
using System.Text;
using GalaSoft.MvvmLight;
using PCLStorage;
using System.Diagnostics;
using Xamarin.Forms;
using FormsToolkit;
using Syncfusion.SfChart.XForms;

namespace Microsoft.Band.Portable.iML
{
	public class BandDataSource : ObservableObject
	{

		private StringBuilder data = new StringBuilder();
		private BandClient bandClient;
		//last result storage
		private double[] accel = new double[3];
		private double kFilteringFactor = 0.1;

		private static bool ADAPTIVE_ACCEL_FILTER = true;
		float[] lastAccel = new float[3];
		float[] accelFilter = new float[3];

		public BandDataSource(BandClient bandClient)
		{
			this.bandClient = bandClient;
		}

		public Task SendStartCollectSensorAsync()
		{
			return SendStartCollectSensorAsync(bandClient);
		}

		private async Task SendStartCollectSensorAsync(BandClient bc)
		{
			try
			{
				// Send user a haptic so they know we start to collect sensor data
				await bc.NotificationManager.VibrateAsync(Notifications.VibrationType.TwoToneHigh);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Task SendStopCollectSensorAsync()
		{
			return SendStopCollectSensorAsync(bandClient);
		}

		private async Task SendStopCollectSensorAsync(BandClient bc)
		{
			try
			{
				// Send user a haptic so they know we start to collect sensor data
				await bc.NotificationManager.VibrateAsync(Notifications.VibrationType.TwoToneHigh);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Task StartSensorsAsync()
		{
			return StartSensorsAsync(bandClient);
		}

		private async Task StartSensorsAsync(BandClient bandClient)
		{
			try
			{
				// Start Accelerometer 
				await this.StartAccelerometerReadingAsync();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		private async Task StartAccelerometerReadingAsync()
		{
			//var userConsenting = Sensor as IUserConsentingBandSensor<T>;
			//if (userConsenting == null
			//   || userConsenting.UserConsented == UserConsent.Granted
			//   || await userConsenting.RequestUserConsent())
			//{
			//await SendStartCollectSensorAsync();
			bandClient.SensorManager.Accelerometer.ReadingChanged += HandleAccelerometerChangeReading;
			await bandClient.SensorManager.Accelerometer.StartReadingsAsync(BandSensorSampleRate.Ms32);
			//}
		}

		private async Task StopAccelerometerReadingAsync()
		{
			await bandClient.SensorManager.Accelerometer.StopReadingsAsync();
			bandClient.SensorManager.Accelerometer.ReadingChanged -= HandleAccelerometerChangeReading;
		}

		public async Task StartCollectAccelerometerDataAsync()
		{

			try
			{
				await SendStartCollectSensorAsync();

				var watch = Stopwatch.StartNew();
				await StartAccelerometerReadingAsync();
				// Read 4s then stop reading and save to file
				await Task.Delay(3000);
				Debug.WriteLine("Save here");
				await DependencyService.Get<IFileHelper>().WriteAllText("test1.csv", data.ToString());
				await StopAccelerometerReadingAsync();
				watch.Stop();
				Debug.WriteLine("Execution time: {0}", watch.ElapsedMilliseconds);
				await SendStopCollectSensorAsync();
			}
			catch (BandException ex)
			{
				throw ex;
			}
		}

		//// Event Handling of the Accelerometer will require some extra work to handle
		//// and correct cleanup of memory after we are finished with the data collectionn
		private void HandleAccelerometerChangeReading(object sender, BandSensorReadingEventArgs<BandAccelerometerReading> e)
		{
			var reading = e.SensorReading;

			// Ramping filter
			accel[0] = reading.AccelerationX * kFilteringFactor + accel[0] * (1.0f - kFilteringFactor);
			accel[1] = reading.AccelerationX * kFilteringFactor + accel[0] * (1.0f - kFilteringFactor);
			accel[2] = reading.AccelerationX * kFilteringFactor + accel[0] * (1.0f - kFilteringFactor);

			double x = reading.AccelerationX - accel[0];
			double y = reading.AccelerationX - accel[1];
			double z = reading.AccelerationX - accel[2];

			//// High pass filter
			//float updateFreq = 30; // match this to your update speed
			//float cutOffFreq = 0.9f;
			//float RC = 1.0f / cutOffFreq;
			//float dt = 1.0f / updateFreq;
			//float filterConstant = RC / (dt + RC);
			//float alpha = filterConstant;
			//float kAccelerometerMinStep = 0.033f;
			//float kAccelerometerNoiseAttenuation = 3.0f;

			//if (ADAPTIVE_ACCEL_FILTER)
			//{
			//	float d = clamp(Math.abs(norm(accelFilter[0], accelFilter[1], accelFilter[2]) - norm(accelX, accelY, accelZ)) / kAccelerometerMinStep - 1.0f, 0.0f, 1.0f);
			//	alpha = d * filterConstant / kAccelerometerNoiseAttenuation + (1.0f - d) * filterConstant;
			//}
			//accelFilter[0] = (float)(alpha * (accelFilter[0] + accelX - lastAccel[0]));
			//accelFilter[1] = (float)(alpha * (accelFilter[1] + accelY - lastAccel[1]));
			//accelFilter[2] = (float)(alpha * (accelFilter[2] + accelZ - lastAccel[2]));

			//lastAccel[0] = accelX;
			//lastAccel[1] = accelY;
			//lastAccel[2] = accelZ;

			//data.AppendLine(string.Format("{0:F},{1:F},{2:F}", x, y, z));
			data.AppendLine(string.Format("{0:F},{1:F},{2:F}", reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ));
		}
	}
}
