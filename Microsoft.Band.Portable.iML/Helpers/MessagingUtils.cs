using System;
using FormsToolkit;
namespace Microsoft.Band.Portable.iML
{
	public class MessagingUtils
	{
		public static void SendAlert(string title, string message)
		{
			MessagingService.Current.SendMessage(MessageKeys.Message, new MessagingServiceAlert
			{
				Title = title,
				Message = message,
				Cancel = "OK"
			});
		}
	}
}
