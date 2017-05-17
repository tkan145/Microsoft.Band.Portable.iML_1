using System;
using System.Threading.Tasks;
using FormsToolkit;
using GalaSoft.MvvmLight.Command;
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
	}
}
