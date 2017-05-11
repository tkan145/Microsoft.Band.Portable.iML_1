using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML.Views
{
	public partial class DashboardPage : ContentPage
	{
		private readonly DashboardViewModel _viewModel;
		public DashboardPage()
		{
			InitializeComponent();

			//_viewModel = App.Locator.Main;
			BindingContext = _viewModel;
		}

		protected override async void OnAppearing()
		{
			await _viewModel.Init();
			base.OnAppearing();
		}
	}
}
