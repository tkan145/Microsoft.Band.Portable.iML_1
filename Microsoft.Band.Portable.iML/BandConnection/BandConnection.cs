using System;
using System.Collections.Generic;
using Microsoft.Band.Portable;
using System.Threading.Tasks;
using System.Linq;
using MvvmHelpers;
using Microsoft.Band.Portable.Sensors;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public class BandConnection : ObservableObject
	{
		// Store BandInfo so we can get name of the band later
		public BandDeviceInfo[] pairedBands;
		public BandClient bandClient;

		public BandConnection() { }

		public async Task ConnectBands()
		{
			if (pairedBands == null)
				await this.FindBands();
			else
			{
				this.statusMessage = "Connecting ....";
				try
				{
					bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);
					this.BandConnectionStatus = "Connected";
					this.statusMessage = "You're all set, Band has been configured.";
				}
				catch (BandException ex)
				{
					// Handle band connection Exception
					//var diaglog = DisplayAlert("Alert", "You have been alerted", "Ok");
					await ConnectBands();
				}

				//await AddBandSensor<BandAccelerometerReading>(bandClient.SensorManager.Accelerometer, Accelerometer_ReadingChanged);
			}
		}

		private async Task FindBands()
		{
			try
			{

				// Get the list of Microsoft Bands paired to the phone.
				BandDeviceInfo[] pariedBands = await BandClientManager.Instance.GetPairedBandsAsync() as BandDeviceInfo[];
				if (pariedBands != null && pariedBands.Count() >= 1)
				{
					var bandFound = pariedBands.FirstOrDefault();
					this.BandName = bandFound.Name;
					this.BandConnectionStatus = "No Connection";
					this.statusMessage = "Click connect to connect your devices";
				}
				else
				{
					this.statusMessage = "This app requires a Microsoft Band paired to your device.";
					this.bandConnectionStatus = "No Connection";
					this.bandName = "Not found";
				}
			}
			catch (Exception ex)
			{
				this.StatusMessage = ex.ToString();
				if (this.bandClient != null)
				{
					//this.bandClient.Dispose();
					this.bandClient = null;
				}
			}
		}

		private void UpdateSensorData(string sensorName)
		{
			// Get sensor string from settings here to enable and subrice to specific sensor
		}

		private async Task Accelerometer_ReadingChanged(object sender, BandSensorReadingEventArgs<BandAccelerometerReading> args)
		{
			await Task.Delay(1000);
		}

		private async Task AddBandSensor<T>(IBandSensor<T> sensor, EventHandler<BandSensorReadingEventArgs<T>> ValueChangedEventHandler) where T : IBandSensorReading
		{
			// User granted consent
			// Add sensor

			// Hook up to the sensor ReadingChanged event
			sensor.ReadingChanged += ValueChangedEventHandler;
			await sensor.StartReadingsAsync(BandSensorSampleRate.Ms128);
		}

		private string statusMessage = "";
		public string StatusMessage
		{
			get { return statusMessage; }
			set { SetProperty(ref statusMessage, value); }
		}

		private string bandName = "";
		public string BandName
		{
			get { return bandName; }
			set { SetProperty(ref bandName, value); }
		}

		private string bandConnectionStatus = "";
		public string BandConnectionStatus
		{
			get { return bandConnectionStatus; }
			set { SetProperty(ref bandConnectionStatus, value); }
		}

		public Task DisconnectBand()
		{
			return DisconnectBand(bandClient);
		}

		private async Task DisconnectBand(BandClient bc)
		{
			await bc.DisconnectAsync();
		}

	}
}
