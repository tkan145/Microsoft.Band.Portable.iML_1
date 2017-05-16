using System;
using SQLite;

namespace Microsoft.Band.Portable.iML
{
	public class DBQuery<T>
	{
		private SQLiteAsyncConnection m_db;
		public DBQuery(SQLiteAsyncConnection db)
		{
			m_db = db;
		}

	}
}
