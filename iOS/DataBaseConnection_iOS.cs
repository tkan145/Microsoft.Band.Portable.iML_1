using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Band.Portable.iML.iOS;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataBaseConnection_iOS))]
namespace Microsoft.Band.Portable.iML.iOS
{
	public class DataBaseConnection_iOS : IDatabaseConnection
	{
		public SQLiteAsyncConnection DbConnection(string dbName)
		{
			if (dbName == null)
			{
				throw new ArgumentNullException("dbName");
			}
			string personalFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			string libraryFolder = Path.Combine(personalFolder, "..", "Library");
			var path = Path.Combine(libraryFolder, dbName);
			Debug.WriteLine("Path :", libraryFolder);
			//if (!File.Exists(libraryFolder))
			//{
			//	File.Create(libraryFolder);
			//}

			return new SQLiteAsyncConnection(path);
		}

	}
}
