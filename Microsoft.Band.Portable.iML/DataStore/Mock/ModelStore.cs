using Microsoft.Band.Portable.iML.DataStore.Abstractions;
using Microsoft.Band.Portable.iML;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace Microsoft.Band.Portable.iML.DataStore.Mock
{
	public class ModelStore : BaseStore<iMLModel>, IModelStore
	{
		//List<iMLModel> models;
		//ILogStore logStore;

		//public ModelStore()
		//{
		//	logStore = DependencyService.Get<ILogStore>();
		//}

		//#region ISessionStore implementation
		//public async override Task<iMLModel> GetItemAsync(string id)
		//{
		//	if (!initialized)
		//		await InitializeStore();

		//	return models.FirstOrDefault(s => s.Id == id);
		//}

		public override Task<IEnumerable<iMLModel>> GetItemsAsync(bool forceRefresh = false)
		{
			//if (!initialized)
			//	await InitializeStore();

			//return models as IEnumerable<iMLModel>;

			var models = new[]
				 {
				new iMLModel { Name = "Model 1", Description="Accelerometer",Algorithm = "Q-learning", },
				new iMLModel { Name = "Model 2", Description="Gyroscope",Algorithm = "SARSA" },
				new iMLModel { Name = "Model 3", Description="Distance",Algorithm = "DQN"  },

				};

			return Task.FromResult(models as IEnumerable<iMLModel>);
		}

		//public async Task<IEnumerable<iMLModel>> GetLogsAsync(string logID)
		//{
		//	if (!initialized)
		//		await InitializeStore();
		//	var results = from model in models
		//					se;
		//		return results;
		//}
		//#endregion
		//#region IBaseStore implementation
		//bool initialized = false;
		//public async override Task InitializeStore()
		//{
		//	if (initialized)
		//		return;
		//	initialized = true;

		//	var logs = (await logStore.GetItemsAsync().ConfigureAwait(false)).ToArray();
		//	models = new List<iMLModel>();
		//	int log = 0;
		//	int logCount = 0;

		//	for (int i = 0; i < names.Length; i++)
		//	{
		//		var logsModel = new List<Log>();
		//		logCount++;
		//		for (int j = 0; j < logCount; j++)
		//		{
		//			logsModel.Add(logs[log]);
		//			log++;
		//			if (log >= logs.Length)
		//				log = 0;
		//		}

		//		if (i == 1)
		//			logsModel.Add(models[0].Logs.;

		//		models.Add(new iMLModel
		//		{
		//			Id = i.ToString(),
		//			Logs = logsModel,
		//		});

		//	}
		//}
		//#endregion

		//public Task<iMLModel> GetAppIndexModel(string id)
		//{
		//	return GetItemAsync(id);
		//}

		//string[] names = new string[]
		//{
		//	"Model 1",
		//	"Model 2",
		//	"Model 3"
		//};

		//string[] shortNames = new string[]
		//{
		//	"Accelerometer",
		//	"Gyroscope",
		//	"Distance"
		//};

		//string[] algorithms = new string[]
		//{
		//	"Q-learning",
		//	"SARSA",
		//	"DQN"
		//};
	}
}
