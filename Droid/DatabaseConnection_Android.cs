using System;
using SQLite;
using System.IO;
using Xamarin.Forms;
using Microsoft.Band.Portable.iML.Droid;

[assembly: Dependency(typeof(DatabaseConnection_Android))]
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
			var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
			return new SQLiteAsyncConnection(path);
		}
	}
}
