using System;
using System.Threading.Tasks;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;

namespace Microsoft.Band.Portable.iML.DataStore.Mock
{
	public class BaseStore<T> : IBaseStore<T>
	{
		#region IBaseStore implementation

		public void DropTable()
		{

		}
		public virtual System.Threading.Tasks.Task InitializeStore()
		{
			throw new NotImplementedException();
		}
		public virtual System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
		{
			throw new NotImplementedException();
		}
		public virtual System.Threading.Tasks.Task<T> GetItemAsync(string id)
		{
			throw new NotImplementedException();
		}
		public virtual System.Threading.Tasks.Task<bool> InsertAsync(T item)
		{
			throw new NotImplementedException();
		}
		public virtual System.Threading.Tasks.Task<bool> UpdateAsync(T item)
		{
			throw new NotImplementedException();
		}
		public virtual System.Threading.Tasks.Task<bool> RemoveAsync(T item)
		{
			throw new NotImplementedException();
		}

		public string Identifier => "store";
		#endregion
	}
}
