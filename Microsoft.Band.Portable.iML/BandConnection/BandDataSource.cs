using System;
using System.Threading.Tasks;
//using MvvmHelpers;
using Microsoft.Band.Portable.Sensors;
using System.Text;
using GalaSoft.MvvmLight;
using PCLStorage;
using System.Diagnostics;
using Xamarin.Forms;
namespace Microsoft.Band.Portable.iML
{
	public class BandDataSource : ObservableObject
	{
		private StringBuilder data = new StringBuilder();
		private BandClient bandClient;

		public BandDataSource(BandClient bandClient)
		{

			this.bandClient = bandClient;
			//this.name = name;
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
			await bandClient.SensorManager.Accelerometer.StartReadingsAsync();
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
				await StartAccelerometerReadingAsync();

				// Read 4s then stop reading and save to file
				await Task.Delay(4000);
				Debug.WriteLine("Save here");
				await DependencyService.Get<IFileHelper>().SaveTextFile("test1.csv", data.ToString());
				await StopAccelerometerReadingAsync();
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
			data.AppendLine(string.Format("{0:F},{1:F},{2:F}", reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ));
		}
	}
}
