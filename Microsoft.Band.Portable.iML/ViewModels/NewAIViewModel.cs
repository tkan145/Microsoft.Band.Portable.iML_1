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
			// Save model
			await StoreManager.ModelStore.InsertAsync(Model);
			await Navigation.PopModalAsync();
		}

		#region Property
		private string name;
		public string Name
		{
			get { return name; }
			set { Set(ref name, value); }
		}

		private string description;
		public string Description
		{
			get { return description; }
			set { Set(ref description, value); }
		}

		private double alpha;
		public double Alpha
		{
			get { return alpha; }
			set { Set(ref alpha, value); }
		}

		private double gamma;
		public double Gamma
		{
			get { return gamma; }
			set { Set(ref gamma, value); }
		}

		private string algorithm;
		public string Algorithm
		{
			get { return algorithm; }
			set { Set(ref algorithm, value); }
		}

		private string filter;
		public string Filter
		{
			get { return filter; }
			set { Set(ref filter, value); }
		}
		bool isAdvanceOption;
		public bool IsAdvanceOption
		{
			get { return isAdvanceOption; }
			set { Set(ref isAdvanceOption, value); }
		}
		#endregion
	}
}

