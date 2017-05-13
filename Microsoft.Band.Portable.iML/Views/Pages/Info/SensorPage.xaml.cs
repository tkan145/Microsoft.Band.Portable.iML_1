using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public partial class SensorPage : ContentPage
	{
		SensorViewModel vm;
		public SensorPage()
		{
			InitializeComponent();
			if (Device.RuntimePlatform != Device.iOS)
				ToolbarDone.Icon = "toolbar_close.png";

			ToolbarDone.Command = new Command(async () =>
			{
				//Settings.Current.FavoritesOnly = showFavorites.IsFiltered;
				//Settings.Current.ShowPastSessions = showPast.IsFiltered;
				vm.Save();
				await Navigation.PopModalAsync();
				//if (Device.RuntimePlatform == Device.Android)
				//MessagingService.Current.SendMessage("filter_changed");

			});

			BindingContext = vm = new SensorViewModel(Navigation);
			LoadSensors();
		}

		void LoadSensors()
		{
			vm.LoadCategoriesAsync().ContinueWith((result) =>
				{
					Device.BeginInvokeOnMainThread(() =>
						{
							var allCell = new CategoryCell
							{
								BindingContext = vm.AllCategory
							};

							TableSectionCategories.Add(allCell);

							foreach (var item in vm.Categories)
							{
								Debug.WriteLine("Item", item);
								TableSectionCategories.Add(new CategoryCell
								{
									BindingContext = item
								});
							}
						});
				});
		}
	}
}
