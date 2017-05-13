using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML.Views
{
	public partial class DashboardPage : ContentPage
	{
		DashboardViewModel ViewModel => vm ?? (vm = BindingContext as DashboardViewModel);
		DashboardViewModel vm;
		public DashboardPage()
		{
			InitializeComponent();
			BindingContext = new DashboardViewModel(Navigation);

			ViewModel.Models.CollectionChanged += (sender, e) =>
			{
				var adjust = Device.RuntimePlatform != Device.Android ? 1 : -ViewModel.Models.Count + 1;
				ListViewModels.HeightRequest = (ViewModel.Models.Count * ListViewModels.RowHeight) - adjust;
			};

			ListViewModels.ItemTapped += (sender, e) => ListViewModels.SelectedItem = null;
			ListViewModels.ItemSelected += async (sender, e) =>
									{
										var model = ListViewModels.SelectedItem as iMLModel;
										if (model == null)
											return;
										var modelDetails = new AgentDetailsPage(model);


										await NavigationService.PushAsync(Navigation, modelDetails);
										ListViewModels.SelectedItem = null;
									};
		}

		private void UpdatePage()
		{
			//bool forceRefresh = (DateTime.UtcNow > (ViewModel?.NextForceRefresh ?? DateTime.UtcNow));
			bool forceRefresh = true;
			if (forceRefresh)
			{
				ViewModel.RefreshCommand.Execute(null);
			}
			else
			{

				if (ViewModel.Models.Count == 0)
				{
					ViewModel.LoadModelsCommand.Execute(null);
				}
			}
			//ViewModel.LoadBandsCommand.Execute(null);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			UpdatePage();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			UpdatePage();
		}

		public void OnResume()
		{
			UpdatePage();
		}
	}
}
