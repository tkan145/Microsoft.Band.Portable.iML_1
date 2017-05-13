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
		private string name;



		public BandDataSource(BandClient bandClient, string name)
		{

			this.bandClient = bandClient;
			this.name = name;
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

		public Task StartSensorAsync()
		{
			SendStartCollectSensorAsync();
			return StartSensorAsync(bandClient);
		}

		public async Task StartSensorAsync(BandClient bandClient)
		{
			try
			{
				// Start Acceloremeter data
				await this.StartAccelerometerRateAsync(bandClient);
				// Start gesture detector
				//await this.StartGestureDectectorAsync(bandClient);
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public async Task StopAccelerometerReadingAsync(BandClient bandClient)
		{
			await bandClient.SensorManager.Accelerometer.StopReadingsAsync();
			await SendStopCollectSensorAsync();
			// Save data to csv file here
		}

		public async Task StartAccelerometerRateAsync(BandClient bandClient)
		{
			//bool accelerometerRateConsentGranted;

			//		// Check wheter the user has granted access to the Accelorometer sensor
			//		if (bandClient.SensorManager.Accelerometer.GetCurrentUserConsent() == UserConsent.Granted)
			//		{
			//			accelerometerRateConsentGranted = true;
			//		}
			//		else
			//		{
			//			accelerometerRateConsentGranted = await bandClient.SensorManager.Accelerometer.RequestUserConsentAsync();
			//		}

			//		if (accelerometerRateConsentGranted)	//		{

			// Subscribe to start collect acceloremeter data
			bandClient.SensorManager.Accelerometer.ReadingChanged += HandleAccelerometerChangeReading;
			// Start reading
			await bandClient.SensorManager.Accelerometer.StartReadingsAsync();

			// Read 4s then stop reading and save to file
			await Task.Delay(4000);
			await StopAccelerometerReadingAsync(bandClient);
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
