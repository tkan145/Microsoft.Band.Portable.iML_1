using System;
using System.Threading.Tasks;
//using MvvmHelpers;
using Microsoft.Band.Portable.Sensors;
using System.Text;
using GalaSoft.MvvmLight;

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

		private async Task StartAccelerometerReadingAsync()
		{
			await SendStartCollectSensorAsync();
			await bandClient.SensorManager.Accelerometer.StartReadingsAsync();
		}

		private async Task StopAccelerometerReadingAsync()
		{
			await bandClient.SensorManager.Accelerometer.StopReadingsAsync();
			await SendStopCollectSensorAsync();
		}

		public async Task<string> StartCollectAccelerometerDataAsync()
		{
			// Subscribe to start collect acceloremeter data
			bandClient.SensorManager.Accelerometer.ReadingChanged += HandleAccelerometerChangeReading;
			// Start reading
			try
			{
				await StartAccelerometerReadingAsync();

				// Read 4s then stop reading and save to file
				await Task.Delay(4000);

				await StopAccelerometerReadingAsync();
				//await SaveFileToDiskAsync(data);
				return data.ToString();
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
			data.AppendLine(string.Format("{0:F},{1:F},{3:F}", reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ));
		}
	}
}
