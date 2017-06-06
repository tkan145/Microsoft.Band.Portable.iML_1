using System;
using System.Collections.Generic;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public partial class AgentDetailsPage : ContentPage
	{
		AgentDetailsViewModel ViewModel => vm ?? (vm = BindingContext as AgentDetailsViewModel);
		AgentDetailsViewModel vm;

		public AgentDetailsPage(iMLModel model)
		{
			InitializeComponent();
			BindingContext = new AgentDetailsViewModel(Navigation, model);

            //ButtonTrain.Clicked += async (sender, e) =>
            //{
            //	await NavigationService.PushModalAsync(Navigation, new iMLNavigationPage(new TrainPage(ViewModel.Agent)));
            //};
           

		}

        private void SaveClicked(object sender, EventArgs e)
        {
            sfChart.SaveAsImage("Image.jpg");
        }
		private void UpdatePage()
		{
			//bool forceRefresh = (DateTime.UtcNow > (ViewModel?.NextForceRefresh ?? DateTime.UtcNow));
			bool forceRefresh = true;
			//if (forceRefresh)
			//{
			//	ViewModel.RefreshCommand.Execute(null);
			//}
			//else
			//{

			//	if (ViewModel.Models.Count == 0)
			//	{
			//		ViewModel.LoadModelsCommand.Execute(null);
			//	}
			//}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			//ViewModel.SubscribeMessage();
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
