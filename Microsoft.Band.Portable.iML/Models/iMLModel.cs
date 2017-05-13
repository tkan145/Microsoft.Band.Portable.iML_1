using System;
using System.Collections.Generic;

namespace Microsoft.Band.Portable.iML
{
	public class iMLModel
	{
		//[PrimaryKey]
		public string ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Algorithm { get; set; }
		public virtual ICollection<Log> Logs { get; set; }
		public double Gamma { get; set; }
		public double Accuracy { get; set; }
		public int Eposch { get; set; }

	}

	public class Log
	{
		public string ID { get; set; }
		public string Action { get; set; }
		public float Reward { get; set; }
	}
}
