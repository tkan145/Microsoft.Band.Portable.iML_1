using System;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.iML
{
	public class BaseBandViewModel : BaseViewModel
	{
		public virtual Task PrepareBand()
		{
			return Task.FromResult(true);
		}

		public virtual Task CleanUpBand()
		{
			return Task.FromResult(true);
		}

		public virtual Task DisconnectBand()
		{
			return Task.FromResult(true);
		}
	}
}
