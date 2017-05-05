using System;
using Xamarin.Forms;
using Microsoft.Band.Portable.iML.Views;

namespace Microsoft.Band.Portable.iML
{
	public partial class App : Application
	{
		public static App current;
		public App()
		{
			current = this;
			InitializeComponent();

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					MainPage = new MainNavigationPage(new RootPageiOS());
					break;
				case Device.Android:
					MainPage = new RootPageAndroid();
					break;
				default:
					throw new NotImplementedException();
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
