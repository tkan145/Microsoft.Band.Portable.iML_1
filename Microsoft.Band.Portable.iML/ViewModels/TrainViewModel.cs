using System;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public class TrainViewModel : BaseViewModel
	{
		iMLModel agent;
		public iMLModel Agent
		{
			get { return agent; }
			set { Set(ref agent, value); }
		}

		public TrainViewModel(INavigation navigation, iMLModel model) : base(navigation)
		{
			Agent = model;
		}
	}
}
