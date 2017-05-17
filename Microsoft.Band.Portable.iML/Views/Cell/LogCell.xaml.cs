using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML.Views
{
	public class LogCell : ViewCell
	{
		string logId;
		readonly INavigation navigation;
		public LogCell(string logId, INavigation navigation = null)
		{
			this.logId = logId;

			Height = 60;

			View = new LogCellView();

			StyleId = "disclosure";
			this.navigation = navigation;
		}
	}

	public partial class LogCellView : ContentView
	{
		public LogCellView()
		{
			InitializeComponent();
		}
	}
}
