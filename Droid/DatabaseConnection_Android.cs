using System;
using SQLite;
using System.IO;
namespace Microsoft.Band.Portable.iML.Droid
{
	public class DatabaseConnection_Android : IDatabaseConnection
	{
		public SQLiteAsyncConnection DbConnection(string dbName)
		{
			if (dbName == null)
			{
				throw new ArgumentNullException("dbName");
			}
			var path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);
			return new SQLiteAsyncConnection(path);
		}
	}
}
