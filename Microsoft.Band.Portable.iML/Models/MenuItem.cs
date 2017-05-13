using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public class MenuItem : ObservableObject
	{
		string name;
		public string Name
		{
			get { return name; }
			set { Set(ref name, value); }
		}
		string subtitle;
		public string Subtitle
		{
			get { return subtitle; }
			set { Set(ref subtitle, value); }
		}

		public string Icon { get; set; }
		public string Parameter { get; set; }

		public AppPage Page { get; set; }
		public RelayCommand Command { get; set; }
	}
}
