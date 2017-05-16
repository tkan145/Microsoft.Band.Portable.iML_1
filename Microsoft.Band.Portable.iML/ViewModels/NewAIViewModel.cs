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
		}

		RelayCommand saveCommand;
		public RelayCommand SaveCommand =>
		saveCommand ?? (saveCommand = new RelayCommand(async () => await ExecuteSaveCommand()));

		async Task ExecuteSaveCommand()
		{

			//await Navigation.PopModalAsync(true);
			if (string.IsNullOrWhiteSpace(Name))
				Debug.WriteLine("Model: ", Model.Name);
			else
				Debug.WriteLine(Name);
			var model = new iMLModel();
			model.Name = Name;
			await StoreManager.ModelStore.InsertAsync(model);
			await Navigation.PopModalAsync();
			//await Task.Delay(1000);
		}

		private string name;
		public string Name
		{
			get { return name; }
			set { Set(ref name, value); }
		}

		public iMLModel newModel = new iMLModel();


		bool isAdvanceOption;
		public bool IsAdvanceOption
		{
			get { return isAdvanceOption; }
			set { Set(ref isAdvanceOption, value); }
		}


		public void Save()
		{
			SaveModel();
		}

		private void SaveModel()
		{

		}

		internal void OnDisappearing()
		{
			//_transaction.Dispose();		}

		}
	}
}

