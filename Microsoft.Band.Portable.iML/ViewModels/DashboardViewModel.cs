using System;
using System.Windows.Input;
using System.Collections.Generic;
//using MvvmHelpers;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;
using System.Threading.Tasks;
using FormsToolkit;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Diagnostics;

namespace Microsoft.Band.Portable.iML
{
	public class DashboardViewModel : BaseViewModel
	{
		private BandConnection band;
		public DashboardViewModel(INavigation navigation) : base(navigation)
		{
			NextForceRefresh = DateTime.UtcNow.AddMinutes(30);
			Bands = new ObservableRangeCollection<BandDeviceInfo>();

			band = App.current.Bands;


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
		public ObservableRangeCollection<BandDeviceInfo> Bands { get; private set; }
		public ObservableRangeCollection<ChartDataPoint> XData { get; set; }
		public ObservableRangeCollection<ChartDataPoint> YData { get; set; }
		public ObservableRangeCollection<ChartDataPoint> ZData { get; set; }

		#region Commands
		RelayCommand refreshCommand;
		public RelayCommand RefreshCommand =>
		refreshCommand ?? (refreshCommand = new RelayCommand(async () => await ExecuteRefreshCommandAsync()));

		async Task ExecuteRefreshCommandAsync()
		{
			try
			{
				IsBusy = true;
				var tasks = new Task[]
				{
						ExecuteLoadBandsCommandAsync(),
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

		RelayCommand addModelCommand;
		public RelayCommand AddModelCommand =>
		addModelCommand ?? (addModelCommand = new RelayCommand(async () => await ExecuteAddModelCommandAsync()));

		async Task ExecuteAddModelCommandAsync()
		{
            await NavigationService.PushModalAsync(Navigation, new iMLNavigationPage(new NewAIModelPage()));
		}

		RelayCommand loadBandsCommand;
		public RelayCommand LoadBandsCommand =>
		loadBandsCommand ?? (loadBandsCommand = new RelayCommand(async () => await ExecuteLoadBandsCommandAsync()));

		async Task ExecuteLoadBandsCommandAsync()
		{
			if (LoadingBands)
				return;
			LoadingBands = true;
			FoundBands = BandConfigured = true;
#if DEBUG
			await Task.Delay(1000);
#endif

			try
			{
				//var connection = App.current.Bands;
				await band.FindBands();
				this.StatusMessage = band.StatusMessage;
				this.PairedBand = band.BandName;
				this.Status = band.BandConnectionStatus;

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

		RelayCommand loadModelsCommand;
		public RelayCommand LoadModelsCommand =>
		loadModelsCommand ?? (loadModelsCommand = new RelayCommand(async () => await ExecuteLoadModelsCommandAsync()));

		async Task ExecuteLoadModelsCommandAsync()
		{
			if (LoadingModels)
				return;
			LoadingModels = true;

			try
			{
				NoModels = false;
				Models.Clear();
				RaisePropertyChanged("Models");

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
				MessagingService.Current.SendMessage(MessageKeys.Error, ex);
			}
			finally
			{
				LoadingModels = false;
			}
		}

		RelayCommand configureBandCommand;
		public RelayCommand ConfigureBandCommand =>
		configureBandCommand ?? (configureBandCommand = new RelayCommand(async () => await ExecuteConnectCommand()));

		async Task ExecuteConnectCommand()
		{
			//if (band.bandClient.IsConnected)
			//{
			//	try
			//	{
			//		await band.DisconnectBand();
			//	}
			//	catch (Exception ex)
			//	{
			//		this.statusMessage = "Disconnect" + ex;
			//	}
			//}
			//else
			//{
			//try
			//{
			// Connect must be called on a background thread.
			//var result =
			await band.ConnectBands();

			//// callback that must be handled on the UI thread
			//if (result != band.bandClient.IsConnected)
			//{
			//	this.statusMessage = "Connection failed: result=" + result;
			//	return;
			//}
			this.StatusMessage = band.StatusMessage;
			this.ButtonText = "Disconnect the Band";
			this.Status = band.BandConnectionStatus;
			//}
			//catch (Exception ex)
			//{
			//	this.StatusMessage = "Connect" + ex;
			//} 
			//}

		}
		#endregion

		#region Property
		#region Band
		bool loadingBands;
		public bool LoadingBands
		{
			get { return loadingBands; }
			set { Set(ref loadingBands, value); }
		}
		string statusMessage;
		public string StatusMessage
		{
			get { return statusMessage; }
			set { Set(ref statusMessage, value); }
		}

		bool foundBands;
		public bool FoundBands
		{
			get { return foundBands; }
			set { Set(ref foundBands, value); }
		}

		string status;
		public string Status
		{
			get { return status; }
			set { Set(ref status, value); }
		}

		string pariedBand;
		public string PairedBand
		{
			get { return pariedBand; }
			set { Set(ref pariedBand, value); }
		}

		string buttonText = "Setup Connection";
		public string ButtonText
		{
			get { return buttonText; }
			set { Set(ref buttonText, value); }
		}

		bool bandConfigured;
		public bool BandConfigured
		{
			get { return bandConfigured; }
			set { Set(ref bandConfigured, value); }
		}
		#endregion
		#region Model
		bool loadingModels;
		public bool LoadingModels
		{
			get { return loadingModels; }
			set { Set(ref loadingModels, value); }
		}

		iMLModel selectedModel;
		public iMLModel SelectedModel
		{
			get { return selectedModel; }
			set
			{
				selectedModel = value;
				RaisePropertyChanged();
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
			set { Set(ref noModels, value); }
		}
		#endregion
		#endregion
	}
}
