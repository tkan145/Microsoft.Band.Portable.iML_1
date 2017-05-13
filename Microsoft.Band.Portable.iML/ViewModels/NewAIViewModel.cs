using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public class NewAIViewModel : BaseViewModel
	{
		//private Transaction _transaction;
		//private Realm _realm;
		internal INavigation Navigation { get; set; }
		public iMLModel Model { get; private set; }

		//      public NewRIViewModel(DataObjects.iMLModel model, Transaction transaction)
		//{
		//          //_realm = Realm.GetInstance();
		//          //_transaction = transaction;
		//          Model = model;
		//}

		RelayCommand saveCommand;
		public RelayCommand SaveCommand =>
		saveCommand ?? (saveCommand = new RelayCommand(async () => await ExecuteSaveCommand()));

		async Task ExecuteSaveCommand()
		{
			//_transaction.Commit();
			await Navigation.PopModalAsync(true);
		}

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

		List<string> algorithms = new List<string>
		{
		"Afghanistan",
		"Albania",
		"Algeria",
		"Andorra",
		"Angola",



		};
		public List<string> Algorithms => algorithms;





		internal void OnDisappearing()
		{
			//_transaction.Dispose();
		}
	}
}
