using System;
namespace Microsoft.Band.Portable.iML
{
	public class AboutViewModel : SettingsViewModel
	{
		public ObservableRangeCollection<Grouping<string, MenuItem>> MenuItems { get; }
		public ObservableRangeCollection<MenuItem> InfoItems { get; } = new ObservableRangeCollection<MenuItem>();
		public ObservableRangeCollection<MenuItem> BandItems { get; } = new ObservableRangeCollection<MenuItem>();

		public AboutViewModel()
		{
			AboutItems.Clear();
			AboutItems.Add(new MenuItem { Name = "About this app", Icon = "icon_venue.png" });
			SensorItems.Clear();
			SensorItems.Add(new MenuItem { Name = "Sensors", Icon = "icon_venue.png" });
		}
	}
}
