using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.iML.DataStore.Abstractions
{
	public interface IBaseStore<T>
	{
		Task InitializeStore();
		Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
		Task<T> GetItemAsync(string id);
		Task<bool> InsertAsync(T item);
		Task<bool> UpdateAsync(T item);
		Task<bool> RemoveAsync(T item);

		void DropTable();

		string Identifier { get; }
	}
}
