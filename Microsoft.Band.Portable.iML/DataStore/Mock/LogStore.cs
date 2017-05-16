using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Band.Portable.iML;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;

namespace Microsoft.Band.Portable.iML.DataStore.Mock
{
	public class LogStore : BaseStore<Log>, ILogStore
	{
		IEnumerable<Log> logs;

		#region ILogStore implementation

		public async override Task<Log> GetItemAsync(string id)
		{
			if (!initialized)
				await InitializeStore();
			return logs.FirstOrDefault(s => s.Id == id);
		}

		public async override Task<IEnumerable<Log>> GetItemsAsync(bool forceRefresh = false)
		{
			if (!initialized)
				await InitializeStore();

			return logs;
		}

		#endregion

		#region IBaseStore implementation

		bool initialized;
		public override Task InitializeStore()
		{
			if (initialized)
				return Task.FromResult(true);

			initialized = true;
			logs = SampleData.GetLogs();

			return Task.FromResult(true);
		}

		#endregion

	}

	static class SampleData
	{
		static Random random = new Random();

		public static IEnumerable<Log> GetLogs() => Generate();

		static IEnumerable<Log> Generate()
		{
			random = new Random(0);
			for (int i = 0; i < 50; i++)
			{
				float accuracy = random.NextFloat();
				int reward = random.Next(1, 100);
				var action = ChoosenActions[random.Next(0, ChoosenActions.Length - 1)];
				var eposch = random.Next(100, 1000);
				yield return new Log
				{
					Id = i.ToString(),
					//Accuracy = accuracy,
					Reward = reward,
					Action = action,
					//Espoch = eposch
				};
			}

		}

		static float NextFloat(this Random rd)
		{
			double mantissa = (rd.NextDouble() * 2.0) - 1.0;
			double exponent = Math.Pow(2.0, random.Next(-126, 128));
			return (float)(mantissa * exponent);
		}

		readonly static string[] ChoosenActions =
		{
		"Up",
		"Down",
		"Up Left",
		"Up Right",
		"Down Left",
		"Down Right",
		};

	}
}
