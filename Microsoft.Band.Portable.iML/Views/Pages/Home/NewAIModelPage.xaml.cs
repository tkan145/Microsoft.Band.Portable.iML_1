using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public partial class NewAIModelPage : ContentPage
	{
		NewAIViewModel ViewModel => vm ?? (vm = BindingContext as NewAIViewModel);
		NewAIViewModel vm;
		public NewAIModelPage()
		{
			InitializeComponent();
			this.BindingContext = vm = new NewAIViewModel(Navigation);
			////viewModel.Navigation = Navigation;
			////BindingContext = vm = new NewRIViewModel(Navigation);

			tableView.Root.Remove(AdvanceOptionSection);

			vm.PropertyChanged += (senser, args) =>
			{
				if (args.PropertyName == "IsAdvanceOption")
				{
					if (vm.IsAdvanceOption && tableView.Root.IndexOf(AdvanceOptionSection) == -1)
					{
						tableView.Root.Add((AdvanceOptionSection));
					}
					if (!vm.IsAdvanceOption && tableView.Root.IndexOf(AdvanceOptionSection) != -1)
					{
						tableView.Root.Remove(AdvanceOptionSection); ;
					}
				}
			};

			ToolbarCancel.Command = new Command(async () =>
			{
				await Navigation.PopModalAsync();
			});
			//ToolbarSave.Command = new Command(async () =>
			//{
			//	//vm.Save();
			//	await Navigation.PopModalAsync();
			//});


		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			(BindingContext as NewAIViewModel)?.OnDisappearing();
			BindingContext = null;
		}

	}
}
