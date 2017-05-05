using System.Diagnostics;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML.Views
{
	public class CardView : Frame
	{
		public CardView()
		{
			Padding = 0;
			if (Device.RuntimePlatform == Device.iOS)
			{
				HasShadow = false;
				OutlineColor = Color.Transparent;
				BackgroundColor = Color.Transparent;
			}
		}
	}
}
