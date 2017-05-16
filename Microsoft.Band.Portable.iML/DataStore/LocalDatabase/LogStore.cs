using System;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;

namespace Microsoft.Band.Portable.iML.DataStore.Local
{
	public class LogStore : BaseStore<Log>, ILogStore
	{
		public override string Identifier => "LogStore";
		public LogStore()
		{
		}
	}
}
