using System;
using System.Threading.Tasks;
using Microsoft.Band.Portable;


namespace Microsoft.Band.Portable.iML
{
	public class BandInformation
	{
		public string Name { get; private set; }
		public string Firmware { get; private set; }
		public string Hardware { get; private set; }

		public async Task<string> RetrieveInfo(BandDeviceInfo bandInfo, BandClient client)
		{
			Name = bandInfo.Name;
			Firmware = await client.GetFirmwareVersionAsync();
			Hardware = await client.GetHardwareVersionAsync();
			return string.Format("Connected to : {0}" +
								 " \n Firmware : {1} \n Hardware : {2}", Name, Firmware, Hardware);
		}

		public BandInformation()
		{
		}
	}
}
