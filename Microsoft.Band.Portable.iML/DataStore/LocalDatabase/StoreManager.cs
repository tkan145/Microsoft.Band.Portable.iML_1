using System;
using System.Threading.Tasks;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;
using Xamarin.Forms;
using SQLite;

namespace Microsoft.Band.Portable.iML.DataStore.Local
{

	public class StoreManager : IStoreManager
	{
		public static SQLiteAsyncConnection store { get; set; }

		public StoreManager()
		{
			//store = new SQLiteAsyncConnection();
		}

		/// <summary>
		/// Drops all tables from the database and updated DB Id
		/// </summary>
		/// <returns>The everything async.</returns>
		public Task DropEverythingAsync()
		{
			Settings.UpdateDataBaseId();
			ModelStore.DropTable();
			LogStore.DropTable();
			return Task.FromResult(true);
		}

		public bool IsInitialized { get; private set; }
		#region IStoreManager Implementation
		//object locker = new object();
		internal static AsyncLock locker = new AsyncLock();
		public async Task InitializeAsync()
		{
			using (await locker.LockAsync())
			{
				if (IsInitialized)
					return;
				IsInitialized = false;
				var dbId = Settings.DatabaseId;
				var dbName = $"store{dbId}.db3";
				//	//store = new SQLiteConnection(path);
				store = DependencyService.Get<IDatabaseConnection>().DbConnection(dbName);
				await store.CreateTableAsync<iMLModel>(); ;
				await store.CreateTableAsync<Log>();
			}
			// Init store
		}

		IModelStore modelStore;
		public IModelStore ModelStore => modelStore ?? (modelStore = DependencyService.Get<IModelStore>());

		ICategoryStore categoryStore;
		public ICategoryStore CategoryStore => categoryStore ?? (categoryStore = DependencyService.Get<ICategoryStore>());

		ILogStore logStore;
		public ILogStore LogStore => logStore ?? (logStore = DependencyService.Get<ILogStore>());
		#endregion

		public class StoreSettings
		{
			public const string StoreSettingsId = "store_settings";
			public StoreSettings()
			{
				Id = StoreSettingsId;
			}

			public string Id { get; set; }
		}
	}
}
