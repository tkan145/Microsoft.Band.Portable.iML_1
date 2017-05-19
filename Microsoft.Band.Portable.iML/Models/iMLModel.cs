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
			Algorithm = "";         // qlearn/sarsa
			Alpha = 0.1;            // Learning Rate
			Gamma = 0.9;            // Discount factor
			Epsilon = 0.2;          // initial epsilon for epsilon-greedy policy [0,1] 
			MaxReward = 1;
			MinReward = -1;
			Eposch = 0;
			AccumulativeReward = 0;
		}

		//experience_add_every = 10; // number of time steps before we add another experience to replay memory
		//experience_size = 5000; // size of experience replay memory
		//learning_steps_per_iteration = 20;
		//tderror_clamp = 1.0; // for robustness
		//num_hidden_units = 100 // number of neurons in hidden layer

		//[PrimaryKey]
		//public string ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public float MaxReward { get; set; }
		public float MinReward { get; set; }
		//public virtual ICollection<Log> Logs { get; set; }
		public double Alpha { get; set; }
		public double Gamma { get; set; }
		public double Epsilon { get; set; }
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
