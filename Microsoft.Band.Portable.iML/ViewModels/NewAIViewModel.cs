using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public class NewAIViewModel : BaseViewModel
	{
		//internal INavigation Navigation { get; set; }
		public iMLModel Model { get; set; }
		public NewAIViewModel(INavigation navigation) : base(navigation)
		{
			Model = new iMLModel();
			Model.IsApplyFilter = IsAdvanceOption;
		}

		RelayCommand saveCommand;
		public RelayCommand SaveCommand =>
		saveCommand ?? (saveCommand = new RelayCommand(async () => await ExecuteSaveCommand()));

		async Task ExecuteSaveCommand()
		{
			// Save model
			await StoreManager.ModelStore.InsertAsync(Model);
			await Navigation.PopModalAsync();
		}

		bool isAdvanceOption;
		public bool IsAdvanceOption
		{
			get { return isAdvanceOption; }
			set { Set(ref isAdvanceOption, value); }
		}
	}
}

