using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using FormsToolkit;
namespace Microsoft.Band.Portable.iML
{
	public class QLearning //: LearningAgent
	{
		// amount of possible states
		private int states;

		// amount of possible actions
		private int actions;

		// q-values
		private double[][] qvalues;

		// exploration policy
		private IExplorationPolicy explorationPolicy;

		// discount factor
		private double discountFactor = 0.95;

		// learning rate
		private double learningRate = 0.25;

		public int StatesCount
		{
			get { return states; }
		}

		public int ActionsCount
		{
			get { return actions; }

		}

		public IExplorationPolicy ExplorationPolicy
		{
			get { return explorationPolicy; }
			set { explorationPolicy = value; }
		}

		public double LearningRate
		{
			get { return learningRate; }
			set
			{
				if (value < 0 || value > 1.0)
					throw new ArgumentOutOfRangeException("Argument should be between 0 and 1.");
				learningRate = value;
			}
		}

		public double DiscountFactor
		{
			get { return discountFactor; }
			set
			{
				if (value < 0 || value > 1.0)
					throw new ArgumentOutOfRangeException("Discount factor should be between 0 and 1.");
				discountFactor = value;
			}
		}

		public QLearning(int states, int actions, IExplorationPolicy explorationPolicy) :
					this(states, actions, explorationPolicy, true)
		{
		}

		public QLearning(int states, int actions, IExplorationPolicy explorationPolicy, bool randomize)
		{
			this.states = states;
			this.actions = actions;
			this.explorationPolicy = explorationPolicy;

			// create Q-array
			qvalues = new double[states][];
			for (int i = 0; i < states; i++)
			{
				qvalues[i] = new double[actions];
			}

			// do randomization
			if (randomize)
			{
				Random rand = new Random();

				for (int i = 0; i < states; i++)
				{
					for (int j = 0; j < actions; j++)
					{
						qvalues[i][j] = rand.NextDouble() / 10;
					}
				}
			}

		}

		public int GetAction(int state)
		{
			return explorationPolicy.ChooseAction(qvalues[state]);
		}

		public void UpdateState(int previousState, int action, double reward, int nextState)
		{
			// next state's action estimations
			double[] nextActionEstimations = qvalues[nextState];

			// find maximum expected summary reward from the next state
			double maxNextExpectedReward = nextActionEstimations[0];

			for (int i = 1; i < actions; i++)
			{
				if (nextActionEstimations[i] > maxNextExpectedReward)
					maxNextExpectedReward = nextActionEstimations[i];
			}

			// previous state's action estimations
			double[] previousActionEstimations = qvalues[previousState];

			// update expexted summary reward of the previous state
			// Q(state,action)= Q(state,action) + alpha * (R(state,action) + gamma * Max(next state, all actions) - Q(state,action))
			previousActionEstimations[action] *= (1.0 - learningRate);
			previousActionEstimations[action] += (learningRate * (reward + discountFactor * maxNextExpectedReward) - previousActionEstimations[action]);
		}
	}
}

