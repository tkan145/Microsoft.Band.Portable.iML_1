using System;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.iML.DataStore.Abstractions
{
	public interface IStoreManager
	{
		bool IsInitialized { get; }
		Task InitializeAsync();

		// Stores
		IModelStore ModelStore { get; }
		ICategoryStore CategoryStore { get; }
		ILogStore LogStore { get; }

		Task DropEverythingAsync();
	}
}
