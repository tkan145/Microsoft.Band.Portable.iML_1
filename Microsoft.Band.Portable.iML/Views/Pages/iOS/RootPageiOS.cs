using System;
using Xamarin.Forms;
namespace Microsoft.Band.Portable.iML.Views
{
	public class RootPageiOS : TabbedPage
	{
		public RootPageiOS()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			Children.Add(new MainNavigationPage(new DashboardPage()));
			Children.Add(new MainNavigationPage(new AboutPage()));
		}
	}
}
