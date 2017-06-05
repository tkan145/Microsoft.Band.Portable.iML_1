using System;
using System.Threading.Tasks;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;
using Newtonsoft.Json;

namespace Microsoft.Band.Portable.iML.DataStore.Local
{
	public class LogStore : BaseStore<Log>, ILogStore
	{
		public override string Identifier => "LogStore";
		public async Task<bool> GetLogs(string modelId)
		{
			await InitializeStore().ConfigureAwait(false);
			// Load from json file here;
			var items = await Table.Where(s => s.ModelId == modelId).ToListAsync().ConfigureAwait(false);
			return items.Count > 0;

		}

		public Task DropLogs()
		{
			return Task.FromResult(true);
		}
	}
}
