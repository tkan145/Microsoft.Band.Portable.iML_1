using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;

namespace Microsoft.Band.Portable.iML
{
	public class DashboardViewModel : BaseViewModel
	{
		private readonly IFileService _fileService;
		private bool _isLoadingData;
		public DashboardViewModel()
		{
		}

		public bool IsLoadingData
		{
			get { return _isLoadingData; }
			set
			{
				if (value == _isLoadingData) return;
				_isLoadingData = value;
				OnPropertyChanged(() => IsLoadingData);
			}
		}

		public ObservableRangeCollection<Model> Models { get; set; }

		public ICommand UpdateCompaniesCommand { get; set; }

		public async Task Init()
		{
			await UpdateModelsList();
		}

		private async void UpdateModels()
		{
			IsLoadingData = true;

			await _fileService.UpdateItemsAsync();
			await UpdateModelsList();

			IsLoadingData = false;
		}

		private async Task UpdateModelsList()
		{
			var models = await _fileService.GetItemsAsync();

			Models.Clear();

			foreach (var each in models)
			{
				Models.Add(each);
			}
		}
	}
}