using System;
namespace Microsoft.Band.Portable.iML
{
	public interface IDatabaseConnection
	{
		SQLite.SQLiteAsyncConnection DbConnection(string dbName);
	}
}
