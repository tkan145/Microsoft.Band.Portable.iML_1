using System;
using System.Threading.Tasks;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML.DataStore.Mock
{
	public class StoreManager : IStoreManager
	{
		#region IStoreManager implementation

		public Task<bool> SyncAllAsync(bool syncUserSpecific)
		{
			return Task.FromResult(true);
		}

		public bool IsInitialized { get { return true; } }
		public Task InitializeAsync()
		{
			return Task.FromResult(true);
		}

		#endregion
		public Task DropEverythingAsync()
		{
			return Task.FromResult(true);
		}

		IModelStore modelStore;
		public IModelStore ModelStore => modelStore ?? (modelStore = DependencyService.Get<IModelStore>());

		ICategoryStore categoryStore;
		public ICategoryStore CategoryStore => categoryStore ?? (categoryStore = DependencyService.Get<ICategoryStore>());

		ILogStore logStore;
		public ILogStore LogStore => logStore ?? (logStore = DependencyService.Get<ILogStore>());
	}
}
