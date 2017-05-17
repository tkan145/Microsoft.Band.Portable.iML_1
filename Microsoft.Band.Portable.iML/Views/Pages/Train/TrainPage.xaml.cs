using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public partial class TrainPage : ContentPage
	{
		TrainViewModel vm;
		public TrainPage(iMLModel model)
		{
			InitializeComponent();

			BindingContext = vm = new TrainViewModel(Navigation, model);
			if (Device.RuntimePlatform != Device.iOS)
				ToolbarDone.Icon = "toolbar_close.png";

			ToolbarDone.Command = new Command(async () =>
				{
					if (vm.IsBusy)
						return;

					await Navigation.PopModalAsync();
				});
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}
