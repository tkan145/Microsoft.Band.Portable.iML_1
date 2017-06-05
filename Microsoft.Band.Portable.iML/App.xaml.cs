using System;
using Xamarin.Forms;
using Microsoft.Band.Portable.iML.Views;
using FormsToolkit;

namespace Microsoft.Band.Portable.iML
{
	public partial class App : Application
	{
		public static App current;
		public static ILogger logger;
		public BandConnection Bands { get; set; }
		public App()
		{
			current = this;
			InitializeComponent();
			this.Bands = new BandConnection();
			logger = DependencyService.Get<ILogger>();
			BaseViewModel.Init();
			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					MainPage = new MainNavigationPage(new RootPageiOS());
					break;
				case Device.Android:
					MainPage = new RootPageAndroid();
					break;
				default:
					throw new NotImplementedException();
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			MessagingService.Current.Subscribe<MessagingServiceAlert>(MessageKeys.Message, async (m, info) =>
		  	{
				  var task = Application.Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);

				  if (task == null)
					  return;

				  await task;
				  info?.OnCompleted?.Invoke();
		  	});
			MessagingService.Current.Subscribe<MessagingServiceChoice>(MessageKeys.Choice, async (m, q) =>
			{
				var task = Application.Current?.MainPage?.DisplayActionSheet(q.Title, q.Cancel, q.Destruction, q.Items);
				if (task == null)
					return;
				var result = await task;
				q?.OnCompleted?.Invoke(result);
			});
			MessagingService.Current.Subscribe<MessagingServiceQuestion>(MessageKeys.Question, async (m, q) =>
			   {
				   var task = Application.Current?.MainPage?.DisplayAlert(q.Title, q.Question, q.Positive, q.Negative);
				   if (task == null)
					   return;
				   var result = await task;
				   q?.OnCompleted?.Invoke(result);
			   });

		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
