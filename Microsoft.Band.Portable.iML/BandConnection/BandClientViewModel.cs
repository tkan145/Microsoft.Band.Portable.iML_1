using System;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.iML
{
	public class BandClientViewModel : BaseBandClientViewModel
	{
		public BandClientViewModel(BandDeviceInfo info) : base(info, null)
		{
		}

		public string FirmwareVersion { get; private set; }
		public string HardwareVersion { get; private set; }

		public override async Task PrepareBand()
		{
			if (BandClient == null)
			{
				IsLoading = true;
				BandClient = await BandClientManager.Instance.ConnectAsync(BandInfo);
				IsLoading = false;
			}

			await base.PrepareBand();
			FirmwareVersion = await BandClient.GetFirmwareVersionAsync();
			RaisePropertyChanged("FirmwareVersion");
			HardwareVersion = await BandClient.GetHardwareVersionAsync();
			RaisePropertyChanged("HardwareVersion");
		}

		public override async Task DisconnectBand()
		{
			await base.DisconnectBand();
			await BandClient.DisconnectAsync();
		}
	}
}
