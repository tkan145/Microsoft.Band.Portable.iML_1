using System;
namespace Microsoft.Band.Portable.iML
{
	public class SettingsViewModel
	{
		public ObservableRangeCollection<MenuItem> AboutItems { get; } = new ObservableRangeCollection<MenuItem>();
		public ObservableRangeCollection<MenuItem> SensorItems { get; } = new ObservableRangeCollection<MenuItem>();
		public ObservableRangeCollection<MenuItem> TechnologyItems { get; } = new ObservableRangeCollection<MenuItem>();
		public SettingsViewModel()
		{
		}
	}
}
