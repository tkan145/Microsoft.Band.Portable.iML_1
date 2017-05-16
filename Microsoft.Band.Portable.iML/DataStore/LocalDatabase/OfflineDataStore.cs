using System;
using System.Threading.Tasks;

using System.Collections.Generic;
using Xamarin.Forms;

using Microsoft.Band.Portable.iML;

//[assembly:Dependency(typeof(OfflineDataStore))]
namespace XamarinEvolve.Portable.Services
{
	public class OfflineDataStore
	{
		#region IDataStore implementation
		public Task InitializeStore()
		{
			throw new NotImplementedException();
		}
		public Task<iMLModel> GetModelAsync(string id)
		{
			throw new NotImplementedException();
		}
		public Task<IEnumerable<iMLModel>> GetModelsAsync()
		{
			throw new NotImplementedException();
		}
		public Task<IEnumerable<iMLModel>> GetLogModelsAsync(string id)
		{
			throw new NotImplementedException();
		}
		public Task<IEnumerable<Log>> GetLogsAsync()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}

