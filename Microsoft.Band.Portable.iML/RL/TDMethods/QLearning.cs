using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using FormsToolkit;
using Xamarin.Forms;
namespace Microsoft.Band.Portable.iML
{
    public class QLearning //: LearningAgent
    {
        // exploration policy
        private IExplorationPolicy explorationPolicy;
        private Environment env;

        // Learning types
        [Flags]
        public enum LearningTypes
        {
            Q_LEARNING = 1,
            SARSA = 2,
            Q_LAMDA = 3,
        }


        int learningMethod;
        int actionSelection;

        private double discountFactor = 0.95;           // epsilon
        private double learningRate = 0.25;             // alpha
        private double gamma;
        private double lambda;


        private int states;                             // amount of possible states
        private int actions;                            // amount of possible actions
        private int newState;
        private double[][] qvalues;                     // q-values
        private double reward;
        private int epochs;
        public int epochsDone;

        public bool isRunning;
        int[] saPair;



#region Init
        /// <summary>
        /// QLearning Algorithm : Off-policy TD control. Finds the optimal greedy policy
        /// while following an epsilon-greedy policy
        /// </summary>
        /// <param name="env">Current environment</param>
        /// <param name="explorationPolicy">Exploration policy.</param>
        /// <param name="num_episodes">Number of episodes to run for </param>
        /// <param name="epsilon">Chance the sample a random action.</param>
        /// <param name="alpha">Learning rate</param>
        /// <param name="gamma">Gamma.</param>
        /// <param name="lambda">Lambda.</param>

        public QLearning(Environment env, IExplorationPolicy explorationPolicy,double num_episodes, double epsilon, double alpha, double gamma, double lambda)
        {
            this.env = env;
            this.explorationPolicy = explorationPolicy;
            this.learningMethod = (int)LearningTypes.Q_LEARNING;
            this.discountFactor = epsilon;
            this.learningRate = alpha;
            this.lambda = lambda;
            this.gamma = gamma;
            DependencyService.Get<ILogger>().WriteLine("QLearner initialised");
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
#endregion
        #region
        // Execute one epoch
        public void RunEpoch()
        {
            
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
#endregion
        #region Property
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

        public double Gamma
        {
            get { return gamma; }
			set
			{
				if (value < 0 || value > 1.0)
					throw new ArgumentOutOfRangeException("Gamma factor should be between 0 and 1.");
                gamma = value;
			}
        }
#endregion
    }
}

