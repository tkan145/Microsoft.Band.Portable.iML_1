using Microsoft.Band.Portable.iML.DataStore.Abstractions;
using Microsoft.Band.Portable.iML;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Microsoft.Band.Portable.iML.DataStore.Mock
{
	public class SensorCategoryStore : BaseStore<SensorCategory>, ICategoryStore
	{
		public override Task<IEnumerable<SensorCategory>> GetItemsAsync(bool forceRefresh = false)
		{
			var categories = new[]
				 {
					new SensorCategory { Name = "Accelerometer", ShortName="Accelerometer", Color="#66ff66"},
					new SensorCategory { Name = "Gyroscope", ShortName="Gyroscope", Color="#F16EB0"},
					new SensorCategory { Name = "Distance", ShortName="Distance", Color="#7DD5C9" },
					new SensorCategory { Name = "HeartRate", ShortName="HeartRate", Color="#51C7E3"},
					new SensorCategory { Name = "Pedomter", ShortName="Pedomter", Color="#F88F73" },
					new SensorCategory { Name = "Skin Temperature", ShortName="Skin.Temp", Color="#4B637E"},
					new SensorCategory { Name = "UV", ShortName="UV", Color="#9933ff" },
					new SensorCategory { Name = "Band Contact", ShortName="Contact", Color="#ffff00" },
					new SensorCategory { Name = "Calories", ShortName="Calories", Color="#AC9AD3" },
					new SensorCategory { Name = "Galvanic Skin Response", ShortName="Gal.Response", Color="#ff6666" },
					new SensorCategory { Name = "RR Interval", ShortName="RR", Color="#33cccc" },
					new SensorCategory { Name = "Ambient Light", ShortName="Ambient", Color="#0066ff" },
					new SensorCategory { Name = "Barometer", ShortName="Barometer", Color="#996633" },
					new SensorCategory { Name = "Altimeter", ShortName="Altimeter", Color="#ff0066" },
				};

			return Task.FromResult(categories as IEnumerable<SensorCategory>);
		}
	}
}
