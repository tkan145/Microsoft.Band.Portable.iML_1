using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using FormsToolkit;
using GalaSoft.MvvmLight.Command;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public class AgentDetailsViewModel : BaseViewModel
	{
		iMLModel agent;
		public iMLModel Agent
		{
			get { return agent; }
			set { Set(ref agent, value); }
		}

		public AgentDetailsViewModel(INavigation navigation, iMLModel model) : base(navigation)
		{
			Agent = model;
		}

		RelayCommand deleteCommand;
		public RelayCommand DeleteCommand =>
			deleteCommand ?? (deleteCommand = new RelayCommand(async () => await ExecuteDeleteCommandAsync()));
		public async Task ExecuteDeleteCommandAsync()
		{

			if (IsBusy)
				return;

			try
			{
				IsBusy = true;

				//await StoreManager.ModelStore.RemoveAsync(Agent);
				iMLModel result = await StoreManager.ModelStore.GetItemAsync(Agent.Id);
				Debug.WriteLine("Count: {0}", result.Name);
				await Navigation.PopToRootAsync();
			}
			catch (Exception ex)
			{
				MessagingService.Current.SendMessage(MessageKeys.Error, ex);
			}
			finally
			{
				IsBusy = false;
			}
		}


		RelayCommand loadLogCommand;
		public RelayCommand LoadLogCommand =>
				loadLogCommand ?? (loadLogCommand = new RelayCommand(async () => await ExecuteLoadLogCommandAsync()));
		public async Task ExecuteLoadLogCommandAsync()
		{

			if (IsBusy)
				return;

			try
			{
				IsBusy = true;
				//Agent.Logs = await StoreManager.LogStore.Log(Session);
			}
			catch (Exception ex)
			{
				MessagingService.Current.SendMessage(MessageKeys.Error, ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		RelayCommand trainCommand;
		public RelayCommand TrainCommand =>
			trainCommand ?? (trainCommand = new RelayCommand(async () => await ExecuteTrainCommandAsync()));
		public async Task ExecuteTrainCommandAsync()
		{
			if (IsBusy)
				return;

			try
			{
				IsBusy = true;
				var Data = await App.current.Bands.bandData.StartCollectAccelerometerDataAsync();
			}
			catch (Exception ex)
			{
				MessagingService.Current.SendMessage(MessageKeys.Error, ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private string data;
		public string Data
		{
			get { return data; }
			set { Set(ref data, value); }
		}
	}
}
