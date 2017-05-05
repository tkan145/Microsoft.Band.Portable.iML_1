using System;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML.Views
{
	public class MainNavigationPage : NavigationPage
	{
		public MainNavigationPage(Page root) : base(root)
		{
			Init();
			Title = root.Title;
			Icon = root.Icon;
		}

		void Init()
		{
			if (Device.RuntimePlatform == Device.iOS)
			{
				BarBackgroundColor = Color.FromHex("FAFAFA");
			}
			else
			{
				BarBackgroundColor = (Color)Application.Current.Resources["Primary"];
				BarTextColor = (Color)Application.Current.Resources["NavigationText"];
			}
		}
	}
}
