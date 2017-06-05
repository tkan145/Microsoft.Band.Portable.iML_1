using System;
namespace Microsoft.Band.Portable.iML
{
	public class EpsilonGreedyExploration : IExplorationPolicy
	{
		// exploration rate
		private double epsilon;

		// random number generator
		private Random rand = Accord.Math.Random.Generator.Random;

		public double Epsilon
		{
			get { return epsilon; }
			set
			{
				if (value < 0 || value > 1.0)
					throw new ArgumentOutOfRangeException("Epsilon should be between 0 and 1.");
				epsilon = value;
			}
		}

		public EpsilonGreedyExploration(double epsilon)
		{
			Epsilon = epsilon;
		}

		public int ChooseAction(double[] actionEstimates)
		{
			// actions count
			int actionsCount = actionEstimates.Length;

			// find the best action (greedy)
			double maxReward = actionEstimates[0];
			int greedyAction = 0;

			for (int i = 1; i < actionsCount; i++)
			{
				if (actionEstimates[i] > maxReward)
				{
					maxReward = actionEstimates[i];
					greedyAction = i;
				}
			}

			// try to do exploration
			if (rand.NextDouble() < epsilon)
			{
				int randomAction = rand.Next(actionsCount - 1);

				if (randomAction >= greedyAction)
					randomAction++;

				return randomAction;
			}

			return greedyAction;
		}
	}
}
