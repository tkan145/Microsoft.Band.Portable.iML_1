using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;
using Xamarin.Forms;
using System.Linq;
using SQLite;
using System.Collections;
using PCLStorage;
using System.Diagnostics;

namespace Microsoft.Band.Portable.iML.DataStore.Local
{
	public class BaseStore<T> : IBaseStore<T> where T : class, IBaseDataObject, new()
	{
		IStoreManager storeManager;
		public virtual string Identifier => "Items";

		AsyncTableQuery<T> table;
		protected AsyncTableQuery<T> Table
		{
			get { return table ?? (table = StoreManager.store.Table<T>().Where(r => true)); }
		}


		public BaseStore()
		{
		}

		public void DropTable()
		{
			table = null;
		}

		#region IBaseStore Implementation
		public async Task InitializeStore()
		{
			if (storeManager == null)
				storeManager = DependencyService.Get<IStoreManager>();

			if (!storeManager.IsInitialized)
				await storeManager.InitializeAsync().ConfigureAwait(false);
		}


		public virtual async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
		{
			await InitializeStore().ConfigureAwait(false);
			using (await StoreManager.locker.LockAsync())
			{
				return await Table.ToListAsync().ConfigureAwait(false);
			}
		}

		public virtual async Task<T> GetItemAsync(string id)
		{
			await InitializeStore().ConfigureAwait(false);
			var items = await Table.Where(s => s.Id == id).ToListAsync().ConfigureAwait(false);
			if (items == null || items.Count == 0)
				return null;

			return items[0];
		}

		public virtual async Task<bool> InsertAsync(T item)
		{
			await InitializeStore().ConfigureAwait(false);
			using (await StoreManager.locker.LockAsync())
			{
				await StoreManager.store.InsertAsync(item).ConfigureAwait(false);
			}
			return true;
		}

		public virtual async Task<bool> UpdateAsync(T item)
		{
			await InitializeStore().ConfigureAwait(false);
			using (await StoreManager.locker.LockAsync())
			{
				await StoreManager.store.UpdateAsync(item).ConfigureAwait(false);
			}
			return true;
		}

		public virtual async Task<bool> RemoveAsync(T item)
		{
			await InitializeStore().ConfigureAwait(false);
			using (await StoreManager.locker.LockAsync())
			{
				await StoreManager.store.DeleteAsync(item).ConfigureAwait(false);
				return true;
			}
		}
		#endregion
	}

}
