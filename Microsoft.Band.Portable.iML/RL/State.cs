using System;
using System.Collections.Generic;

namespace Microsoft.Band.Portable.iML
{
	public interface State
	{
		List<Object> variableKeys();
		Object get(Object variableKey);
		State copy();
	}
}
