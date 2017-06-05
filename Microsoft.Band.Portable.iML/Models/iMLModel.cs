using System;
using System.Collections.Generic;

namespace Microsoft.Band.Portable.iML
{
	public class iMLModel : BaseDataObject
	{
		public iMLModel()
		{
			//this.Logs = new List<Log>();
			Name = "QLearner";
			Description = "QLearner";
			Algorithm = "";         // qlearn/sarsa
			Alpha = 0.1;            // Learning Rate
			Gamma = 0.9;            // Discount factor
			Epsilon = 0.2;          // initial epsilon for epsilon-greedy policy [0,1] 
			learning_steps_total = 50;
			MaxReward = 1;
			MinReward = -1;
			Eposch = 1;
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
		public bool IsApplyFilter { get; set; }
		public int Eposch { get; set; }
		public double AccumulativeReward { get; set; }
		public List<double> random_action_distribution;

		public int temporal_window = int.MinValue;
		public int experience_size = int.MinValue;
		public int start_learn_threshold = int.MinValue;
		public int learning_steps_total = int.MinValue;
		public int learning_steps_burnin = int.MinValue;
		public int[] hidden_layer_sizes;

		public double epsilon_min = double.MinValue;
		public double epsilon_test_time = double.MinValue;

		public int random_action()
		{
			// a bit of a helper function. It returns a random action
			// we are abstracting this away because in future we may want to 
			// do more sophisticated things. For example some actions could be more
			// or less likely at "rest"/default state.
			return 0;
		}

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
