using System;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public class iMLNavigationPage : NavigationPage
	{
		public iMLNavigationPage(Page root) : base(root)
		{
			Init();
			Title = root.Title;
			Icon = root.Icon;
		}

		public iMLNavigationPage()
		{
			Init();
		}

		void Init()
		{
			if (Device.RuntimePlatform == Device.iOS)
			{
				BarBackgroundColor = Color.FromHex("FAFAFA");
			}
			else
			{
				//BarBackgroundColor = (Color)Application.Current.Resources["Primary"];
				//BarTextColor = (Color)Application.Current.Resources["NavigationText"];
			}
		}
	}
}
