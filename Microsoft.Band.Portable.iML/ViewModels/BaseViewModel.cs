using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;
using Xamarin.Forms;
//using Microsoft.Band.Portable.iML.Helpers;
//using Microsoft.Band.Portable.iML.DataStore.Abstractions;

namespace Microsoft.Band.Portable.iML
{
	public class BaseViewModel : ObservableObject
	{
		string title = string.Empty;

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get { return title; }
			set { Set(ref title, value); }
		}

		string subtitle = string.Empty;

		/// <summary>
		/// Gets or sets the subtitle.
		/// </summary>
		/// <value>The subtitle.</value>
		public string Subtitle
		{
			get { return subtitle; }
			set { Set(ref subtitle, value); }
		}

		string icon = string.Empty;

		/// <summary>
		/// Gets or sets the icon.
		/// </summary>
		/// <value>The icon.</value>
		public string Icon
		{
			get { return icon; }
			set { Set(ref icon, value); }
		}

		bool isBusy;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is busy.
		/// </summary>
		/// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				if (Set(ref isBusy, value))
					IsNotBusy = !isBusy;
			}
		}

		bool isNotBusy = true;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is not busy.
		/// </summary>
		/// <value><c>true</c> if this instance is not busy; otherwise, <c>false</c>.</value>
		public bool IsNotBusy
		{
			get { return isNotBusy; }
			set
			{
				if (Set(ref isNotBusy, value))
					IsBusy = !isNotBusy;
			}
		}

		bool canLoadMore = true;

		/// <summary>
		/// Gets or sets a value indicating whether this instance can load more.
		/// </summary>
		/// <value><c>true</c> if this instance can load more; otherwise, <c>false</c>.</value>
		public bool CanLoadMore
		{
			get { return canLoadMore; }
			set { Set(ref canLoadMore, value); }
		}


		string header = string.Empty;

		/// <summary>
		/// Gets or sets the header.
		/// </summary>
		/// <value>The header.</value>
		public string Header
		{
			get { return header; }
			set { Set(ref header, value); }
		}

		string footer = string.Empty;

		/// <summary>
		/// Gets or sets the footer.
		/// </summary>
		/// <value>The footer.</value>
		public string Footer
		{
			get { return footer; }
			set { Set(ref footer, value); }
		}

		protected INavigation Navigation { get; }

		public BaseViewModel(INavigation navigation = null)
		{
			Navigation = navigation;
		}

		public static void Init(bool mock = true)
		{
			//if (mock)
			//{
			//	DependencyService.Register<IModelStore, DataStore.Mock.ModelStore>();
			//	DependencyService.Register<ICategoryStore, DataStore.Mock.SensorCategoryStore>();
			//	DependencyService.Register<ILogStore, DataStore.Mock.LogStore>();
			//	DependencyService.Register<IStoreManager, DataStore.Mock.StoreManager>();
			//}
			//else
			//{
			//	//DependencyService.Register<IModelStore, DataStore.Offline.ModelStore>();
			//	//DependencyService.Register<ICategoryStore, DataStore.Offline.SensorCategoryStore>();
			//	//DependencyService.Register<ILogStore, DataStore.Offline.LogStore>();
			//	DependencyService.Register<IStoreManager, DataStore.Offline.StoreManager>();
			//	// Register Band datastore here
			//}
		}

		protected IStoreManager StoreManager { get; } = DependencyService.Get<IStoreManager>();

		public Settings Settings
		{
			get { return Settings.Current; }
		}
	}
}
