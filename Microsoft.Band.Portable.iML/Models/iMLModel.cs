using System;
using System.Collections.Generic;

namespace Microsoft.Band.Portable.iML
{
	public class iMLModel : BaseDataObject
	{
		public iMLModel()
		{
			//this.Logs = new List<Log>();
			Name = "";
			Description = "";
			Algorithm = "";
			Alpha = 1;
			Gamma = 0.2;
			MaxReward = 1;
			MinReward = -1;
			Eposch = 0;
			AccumulativeReward = 0;
		}

		//[PrimaryKey]
		//public string ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public float MaxReward { get; set; }
		public float MinReward { get; set; }
		//public virtual ICollection<Log> Logs { get; set; }
		public double Alpha { get; set; }
		public double Gamma { get; set; }
		public string Algorithm { get; set; }
		public string Filter { get; set; }

		public int Eposch { get; set; }
		public double AccumulativeReward { get; set; }

	}

	public class Log : BaseDataObject
	{
		//public string ID { get; set; }
		public string ModelId { get; set; }
		public string State { get; set; }
		public string Action { get; set; }
		public float Reward { get; set; }

	}
}
