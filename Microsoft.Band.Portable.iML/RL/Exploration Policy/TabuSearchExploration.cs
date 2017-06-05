using System;
namespace Microsoft.Band.Portable.iML
{
	public class TabuSearchExploration : IExplorationPolicy
	{
		// total actions count
		private int actions;

		// list of tabu actions
		private int[] tabuActions = null;

		// base exploration policy
		private IExplorationPolicy basePolicy;

		public IExplorationPolicy BasePolicy
		{
			get { return basePolicy; }
			set { basePolicy = value; }
		}

		public TabuSearchExploration(int actions, IExplorationPolicy basePolicy)
		{
			this.actions = actions;
			this.basePolicy = basePolicy;

			// create tabu list
			tabuActions = new int[actions];
		}

		public int ChooseAction(double[] actionEstimates)
		{
			// get amount of non-tabu actions
			int nonTabuActions = actions;
			for (int i = 0; i < actions; i++)
			{
				if (tabuActions[i] != 0)
				{
					nonTabuActions--;
				}
			}

			// allowed actions
			double[] allowedActionEstimates = new double[nonTabuActions];
			int[] allowedActionMap = new int[nonTabuActions];

			for (int i = 0, j = 0; i < actions; i++)
			{
				if (tabuActions[i] == 0)
				{
					// allowed action
					allowedActionEstimates[j] = actionEstimates[i];
					allowedActionMap[j] = i;
					j++;
				}
				else
				{
					// decrease tabu time of tabu action
					tabuActions[i]--;
				}
			}

			return allowedActionMap[basePolicy.ChooseAction(allowedActionEstimates)];
		}

		public void ResetTabuList()
		{
			Array.Clear(tabuActions, 0, actions);
		}

		public void SetTabuAction(int action, int tabuTime)
		{
			tabuActions[action] = tabuTime;
		}
	}
}
