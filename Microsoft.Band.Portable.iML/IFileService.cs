using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Band.Portable.iML
{
	public interface IFileService
	{
		Task<IEnumerable<Model>> GetItemsAsync();
		Task UpdateItemsAsync();
	}
}
