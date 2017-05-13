using System;
using System.Collections.Generic;
using Microsoft.Band.Portable;
using System.Threading.Tasks;
using System.Linq;
//using MvvmHelpers;
using Microsoft.Band.Portable.Sensors;
using Xamarin.Forms;
using GalaSoft.MvvmLight;

namespace Microsoft.Band.Portable.iML
{
	public class BandConnection : ObservableObject
	{
		// Store BandInfo so we can get name of the band later
		public IEnumerable<BandDeviceInfo> pairedBands;
		public BandClient bandClient { get; protected set; }
		public BandDeviceInfo bandInfo;
		public BandConnection()
		{

		}

		public async Task<bool> ConnectBands()
		{
			if (pairedBands == null)
			{
				await FindBands();
			}
			this.statusMessage = "Connecting ....";

			BandClient client = await BandClientManager.Instance.ConnectAsync(this.pairedBands.FirstOrDefault());
			if (client != null)
			{
				this.bandClient = client;
				this.BandConnectionStatus = "Connected";
				this.statusMessage = "You're all set, Band has been configured.";
				return true;
			}
			else
				return false;
		}

		public async Task FindBands()
		{
			try
			{
				// Get the list of Microsoft Bands paired to the phone.
				IEnumerable<BandDeviceInfo> Bands = await BandClientManager.Instance.GetPairedBandsAsync();
				this.pairedBands = Bands;

				if (this.pairedBands == null)
				{
					this.StatusMessage = "This app requires a Microsoft Band paired to your device.";
					this.bandConnectionStatus = "No Connection";
					this.bandName = "Not found";
					return;
				}

				var bandFound = this.pairedBands.FirstOrDefault();
				this.BandName = bandFound.Name;
				this.BandConnectionStatus = "No Connection";
				this.StatusMessage = "Click connect to connect your devices";
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
			set { Set(ref statusMessage, value); }
		}

		private string bandName = "";
		public string BandName
		{
			get { return bandName; }
			set { Set(ref bandName, value); }
		}

		private string bandConnectionStatus = "";
		public string BandConnectionStatus
		{
			get { return bandConnectionStatus; }
			set { Set(ref bandConnectionStatus, value); }
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
