using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Microsoft.Band.Portable.iML
{
	public interface ILocalTable
	{

	}
	public interface ILocalTable<T> : ILocalTable
	{
		//IMobileServiceTableQuery<T> Where(Expression<Func<T, bool>> predicate);
		Task InsertAsync(T instance);
		Task UpdateAsync(T instance);
		Task DeleteAsync(T instance);
		Task<IEnumerable<T>> ToEnumerableAsync();
		Task<List<T>> ToListAsync();
	}
}
