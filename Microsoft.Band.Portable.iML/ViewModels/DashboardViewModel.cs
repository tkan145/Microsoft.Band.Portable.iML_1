using System;
using System.Windows.Input;
using System.Collections.Generic;
using MvvmHelpers;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.iML
{
	public class DashboardViewModel : ViewModelBase
	{
		public DashboardViewModel(INavigation navigation) : base(navigation)
		{
			NextForceRefresh = DateTime.UtcNow.AddMinutes(30);

			// Chart data
			XData = new ObservableRangeCollection<ChartDataPoint>();
			YData = new ObservableRangeCollection<ChartDataPoint>();
			ZData = new ObservableRangeCollection<ChartDataPoint>();

			XData.Add(new ChartDataPoint("1", 0));
			XData.Add(new ChartDataPoint("2", 5));
			XData.Add(new ChartDataPoint("3", 7));
			XData.Add(new ChartDataPoint("4", 6));
			XData.Add(new ChartDataPoint("5", 5.6));
			XData.Add(new ChartDataPoint("6", 3));
			XData.Add(new ChartDataPoint("7", 1));
			XData.Add(new ChartDataPoint("8", -4));
			XData.Add(new ChartDataPoint("9", -10));
			XData.Add(new ChartDataPoint("10", -3.2));

			YData.Add(new ChartDataPoint("1", 3));
			YData.Add(new ChartDataPoint("2", -4));
			YData.Add(new ChartDataPoint("3", 3.9));
			YData.Add(new ChartDataPoint("4", 6));
			YData.Add(new ChartDataPoint("5", -5.6));
			YData.Add(new ChartDataPoint("6", -3));
			YData.Add(new ChartDataPoint("7", 1));
			YData.Add(new ChartDataPoint("8", 4));
			YData.Add(new ChartDataPoint("9", 8.8));
			YData.Add(new ChartDataPoint("10", -3.2));

			ZData.Add(new ChartDataPoint("1", 7));
			ZData.Add(new ChartDataPoint("2", -1));
			ZData.Add(new ChartDataPoint("3", -10));
			ZData.Add(new ChartDataPoint("4", 6));
			ZData.Add(new ChartDataPoint("5", -5.6));
			ZData.Add(new ChartDataPoint("6", 7));
			ZData.Add(new ChartDataPoint("7", 1));
			ZData.Add(new ChartDataPoint("8", -4));
			ZData.Add(new ChartDataPoint("9", 10));
			ZData.Add(new ChartDataPoint("10", -3.2));
		}

		public DateTime NextForceRefresh { get; set; }
		public ObservableRangeCollection<iMLModel> Models { get; } = new ObservableRangeCollection<iMLModel>();
		public IEnumerable<iMLModel> _models { get; }
		public ObservableRangeCollection<ChartDataPoint> XData { get; set; }
		public ObservableRangeCollection<ChartDataPoint> YData { get; set; }
		public ObservableRangeCollection<ChartDataPoint> ZData { get; set; }

		#region Commands
		ICommand refreshCommand;
		public ICommand RefreshCommand =>
		refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommandAsync()));

		async Task ExecuteRefreshCommandAsync()
		{
			try
			{
				IsBusy = true;
				var tasks = new Task[]
				{
						//ExecuteLoadBandsCommandAsync(),
						//ExecuteLoadModelsCommandAsync()
					};

				await Task.WhenAll(tasks);
			}
			catch (Exception ex)
			{
				ex.Data["method"] = "ExecuteRefreshCommandAsync";
			}
			finally
			{
				IsBusy = false;
			}
		}

		ICommand addModelCommand;
		public ICommand AddModelCommand =>
		addModelCommand ?? (addModelCommand = new Command(async () => await ExecuteAddModelCommandAsync()));

		async Task ExecuteAddModelCommandAsync()
		{
			await Task.Delay(1000);
			//var page = new NewRIModelPage(new NewRIViewModel(entry, transaction));
			//Navigation.PushAsync(page
		}

		ICommand loadBandsCommand;
		public ICommand LoadBandsCommand =>
		loadBandsCommand ?? (loadBandsCommand = new Command(async () => await ExecuteLoadBandsCommandAsync()));

		async Task ExecuteLoadBandsCommandAsync()
		{
			if (LoadingBands)
				return;
			LoadingBands = true;
			FoundBands = BandConfigured = false;
#if DEBUG
			await Task.Delay(1000);
#endif

			try
			{
				//var connection = App.current.Bands;
				//await connection.ConnectBands();
				//this.StatusMessage = connection.StatusMessage;
				//this.PairedBand = connection.BandName;
				//this.Status = connection.BandConnectionStatus;

			}
			catch (Exception ex)
			{
				ex.Data["method"] = "ExecuteLoadNotificationsCommandAsync";
			}
			finally
			{
				LoadingBands = false;
			}
		}

		ICommand loadModelsCommand;
		public ICommand LoadModelsCommand =>
		loadModelsCommand ?? (loadModelsCommand = new Command(async () => await ExecuteLoadModelsCommandAsync()));

		async Task ExecuteLoadModelsCommandAsync()
		{
			if (LoadingModels)
				return;
			LoadingModels = true;

			try
			{
				NoModels = false;
				Models.Clear();
				OnPropertyChanged("Models");

#if DEBUG
				await Task.Delay(1000);
#endif

				//Models.ReplaceRange(await StoreManager.ModelStore.GetItemsAsync());

				//if (models != null)
				//	Models.AddRange(models);
				//NoModels = Models.Count == 0;
			}
			catch (Exception ex)
			{
				NoModels = true;
			}
			finally
			{
				LoadingModels = false;
			}
		}

		ICommand configureBandCommand;
		public ICommand ConfigureBandCommand =>
		configureBandCommand ?? (configureBandCommand = new Command(async () => await ExecuteConnectCommand()));

		async Task ExecuteConnectCommand()
		{
			//await App.current.Bands.ConnectBands();
		}
		#endregion

		#region Property
		#region Band
		bool loadingBands;
		public bool LoadingBands
		{
			get { return loadingBands; }
			set { SetProperty(ref loadingBands, value); }
		}
		string statusMessage;
		public string StatusMessage
		{
			get { return statusMessage; }
			set { SetProperty(ref statusMessage, value); }
		}

		bool foundBands;
		public bool FoundBands
		{
			get { return foundBands; }
			set { SetProperty(ref foundBands, value); }
		}

		string status;
		public string Status
		{
			get { return status; }
			set { SetProperty(ref status, value); }
		}

		string pariedBand;
		public string PairedBand
		{
			get { return pariedBand; }
			set { SetProperty(ref pariedBand, value); }
		}

		bool bandConfigured;
		public bool BandConfigured
		{
			get { return bandConfigured; }
			set { SetProperty(ref bandConfigured, value); }
		}
		#endregion
		#region Model
		bool loadingModels;
		public bool LoadingModels
		{
			get { return loadingModels; }
			set { SetProperty(ref loadingModels, value); }
		}

		iMLModel selectedModel;
		public iMLModel SelectedModel
		{
			get { return selectedModel; }
			set
			{
				selectedModel = value;
				OnPropertyChanged();
				if (selectedModel == null)
					return;
				//MessagingService.Current.SendMessage(MessageKeys.NavigateToSession, selectedSession);
				SelectedModel = null;
			}
		}

		bool noModels;
		public bool NoModels
		{
			get { return noModels; }
			set { SetProperty(ref noModels, value); }
		}
		#endregion
		#endregion
	}
}
