using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML.Views
{
	public partial class AboutPage : ContentPage
	{
		AboutViewModel vm;
		public AboutPage()
		{
			InitializeComponent();

			BindingContext = vm = new AboutViewModel();

			var adjust = Device.RuntimePlatform != Device.Android ? 1 : -vm.AboutItems.Count + 1;
			ListViewSensor.HeightRequest = (vm.SensorItems.Count * ListViewSensor.RowHeight) - adjust;
			ListViewSensor.ItemTapped += (sender, e) => ListViewSensor.SelectedItem = null;
			ListViewAbout.HeightRequest = (vm.AboutItems.Count * ListViewAbout.RowHeight) - adjust;
			ListViewAbout.ItemTapped += (sender, e) => ListViewAbout.SelectedItem = null;
			ListViewInfo.HeightRequest = (vm.InfoItems.Count * ListViewInfo.RowHeight) - adjust;

			ListViewBandInfo.HeightRequest = (vm.BandItems.Count * ListViewBandInfo.RowHeight) - adjust;
			ListViewBandInfo.ItemTapped += (sender, e) => ListViewBandInfo.SelectedItem = null;

			ListViewSensor.ItemSelected += async (sender, e) =>
			{
				if (ListViewSensor.SelectedItem == null)
					return;


				await Navigation.PushModalAsync(new iMLNavigationPage(new SensorPage()));

				ListViewAbout.SelectedItem = null;
			};

			ListViewAbout.ItemSelected += async (sender, e) =>
				{
					if (ListViewAbout.SelectedItem == null)
						return;
					await Task.Delay(1000);
					//App.Logger.TrackPage(AppPage.Settings.ToString());
					//await NavigationService.PushAsync(Navigation, new SettingsPage());

					ListViewAbout.SelectedItem = null;
				};
		}
	}
}
