using System;

namespace Microsoft.Band.Portable.iML.Droid
{
	public class Logger : ILogger
	{
		public void WriteLine(string text)
		{
			Android.Util.Log.WriteLine(Android.Util.LogPriority.Info, text, null);
		}

		public void WriteLineTime(string text, params object[] args)
		{
			Android.Util.Log.WriteLine(Android.Util.LogPriority.Info, DateTime.Now.Ticks + " " +
									   String.Format(text, args), null);
		}
	}
}
