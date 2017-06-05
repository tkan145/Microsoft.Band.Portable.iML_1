using System;
namespace Microsoft.Band.Portable.iML
{
	public interface Action
	{
		String actionName();
		Action copy();
	}
}
