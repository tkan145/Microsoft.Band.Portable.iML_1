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

		// Get all band sensor data 
		public BandDataSource bandData;

		public BandConnection()
		{

		}

		public async Task<bool> FindBands()
		{
			try
			{
				// Get the list of Microsoft Bands paired to the phone.
				IEnumerable<BandDeviceInfo> Bands = await BandClientManager.Instance.GetPairedBandsAsync();
				this.pairedBands = Bands;

				// If we can not find any band, then update the message
				if (this.pairedBands == null || this.pairedBands.Count() < 1)
				{
					this.StatusMessage = "This app requires a Microsoft Band paired to your device.";
					this.bandConnectionStatus = "No Connection";
					this.bandName = "Not found";
					return false;
				}

				var bandFound = this.pairedBands.FirstOrDefault();
				this.BandName = bandFound.Name;
				this.BandConnectionStatus = "No Connection";
				this.StatusMessage = "Click connect to connect your devices";
				return true;


			}
			catch (Exception ex)
			{
				this.StatusMessage = ex.ToString();
				if (this.bandClient != null)
				{
					this.bandClient = null;
				}
				return false;
			}
		}
		public async Task ConnectBands()
		{
			if (pairedBands == null)
			{
				await FindBands();
			}
			this.statusMessage = "Connecting ....";
			await Task.Delay(2000);
			BandClient client = await BandClientManager.Instance.ConnectAsync(this.pairedBands.FirstOrDefault());
			if (client == null)
			{
				return;
			}
			else
			{
				this.bandClient = client;
				this.bandData = new BandDataSource(this.bandClient);
				this.BandConnectionStatus = "Connected";
				this.statusMessage = "You're all set, Band has been configured.";
			}
		}

		public Task DisconnectBand()
		{
			this.BandConnectionStatus = "Disconnected";
			this.StatusMessage = "Click connect to connect your devices";
			return DisconnectBand(bandClient);
		}

		private async Task DisconnectBand(BandClient bc)
		{
			await bc.DisconnectAsync();
			this.bandClient = null;
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
			await sensor.StartReadingsAsync(BandSensorSampleRate.Ms32);
		}

		#region Property
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

		#endregion

	}
}
