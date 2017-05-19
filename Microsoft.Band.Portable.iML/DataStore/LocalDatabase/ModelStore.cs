using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML.DataStore.Local
{
	public class ModelStore : BaseStore<iMLModel>, IModelStore
	{
		//public override async Task<IEnumerable<iMLModel>> GetItemsAsync(bool forceRefresh = false)
		//{
		//	await InitializeStore().ConfigureAwait(false);

		//	var sessions = await Table.OrderBy(s => s.StartTime).ToListAsync().ConfigureAwait(false);
		//	var favStore = DependencyService.Get<IFavoriteStore>();
		//	await favStore.GetItemsAsync(true).ConfigureAwait(false);//pull latest

		//	foreach (var session in sessions)
		//	{
		//		var isFav = await favStore.IsFavorite(session.Id).ConfigureAwait(false);
		//		session.IsFavorite = isFav;
		//	}

		//	return sessions;
		//}

		//public async Task<IEnumerable<iMLModel>> GetSpeakerSessionsAsync(string speakerId)
		//{

		//	await InitializeStore().ConfigureAwait(false);

		//	var speakers = await GetItemsAsync().ConfigureAwait(false);

		//	return speakers.Where(s => s.Speakers != null && s.Speakers.Any(speak => speak.Id == speakerId))
		//		.OrderBy(s => s.StartTimeOrderBy);
		//}

		//public async Task<IEnumerable<iMLModel>> GetNextSessions()
		//{
		//	var date = DateTime.UtcNow.AddMinutes(-30);//about to start in next 30

		//	var sessions = await GetItemsAsync().ConfigureAwait(false);

		//	var result = sessions.Where(s => s.StartTimeOrderBy > date && s.IsFavorite).Take(2);

		//	var enumerable = result as Session[] ?? result.ToArray();
		//	return enumerable.Any() ? enumerable : null;
		//}

		//public async Task<iMLModel> GetAppIndexSession(string id)
		//{
		//	await InitializeStore().ConfigureAwait(false);
		//	var sessions = await Table.Where(s => s.Id == id || s.RemoteId == id).ToListAsync();

		//	if (sessions == null || sessions.Count == 0)
		//		return null;

		//	return sessions[0];
		//}

		public Task DropModel()
		{
			return Task.FromResult(true);
		}

		public override string Identifier => "iMLModel";
	}

}
