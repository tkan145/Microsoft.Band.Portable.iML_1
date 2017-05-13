using System.Threading.Tasks;
using Microsoft.Band.Portable.iML;

namespace Microsoft.Band.Portable.iML.DataStore.Abstractions
{
	public interface ILogStore : IBaseStore<Log>
	{
		//Task<bool> AddLog(iMLModel model);
	}
}

