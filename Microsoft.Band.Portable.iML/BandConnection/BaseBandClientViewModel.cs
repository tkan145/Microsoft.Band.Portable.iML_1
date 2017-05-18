using System;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.iML
{
	public class BaseBandClientViewModel : BaseBandViewModel
	{
		public BaseBandClientViewModel(BandDeviceInfo info, BandClient bandClient)
		{
			BandClient = bandClient;
			BandInfo = info;
		}


		public override async Task PrepareBand()
		{
			await base.PrepareBand();
			BandName = BandInfo.Name;
			isConnected = BandClient.IsConnected;
		}


		public BandDeviceInfo BandInfo { get; protected set; }
		public BandClient BandClient { get; protected set; }
		public string BandName { get; private set; }
		public bool isConnected { get; private set; }
		private bool isLoading;
		public bool IsLoading
		{
			get { return isLoading; }
			set
			{
				Set(ref isLoading, value);
			}
		}
	}
}
