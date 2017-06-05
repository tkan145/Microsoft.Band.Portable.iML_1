using System;
namespace Microsoft.Band.Portable.iML
{
	public interface ILogger
	{
		void WriteLine(string message);
		void WriteLineTime(string message, params object[] args);
	}
}
