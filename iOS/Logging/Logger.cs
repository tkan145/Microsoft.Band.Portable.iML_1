using System;
using System.Diagnostics;

namespace Microsoft.Band.Portable.iML.iOS
{
	public class Logger : ILogger
	{
		public void WriteLine(string text)
		{
			Debug.WriteLine(text);
		}

		public void WriteLineTime(string text, params object[] args)
		{
			Debug.WriteLine(DateTime.Now.Ticks + " " + String.Format(text, args));
		}
	}
}
