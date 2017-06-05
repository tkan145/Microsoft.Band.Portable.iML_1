using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Microsoft.Band.Portable.iML
{
	public class Episode
	{
		public List<Vector<double>> stateSequence = new List<Vector<double>>();
		public List<Action> actionSequence = new List<Action>();
		public List<double> rewardSequence = new List<double>();

		public Episode()
		{
		}

		public Episode(Vector<double> initialState)
		{
			this.initializeState(initialState);
		}

		public void initializeState(Vector<double> initialState)
		{
			if (this.stateSequence.Count > 0)
			{
				throw new Exception("Cannot initialize episode, because episode is already initialize in a state");
			}
			this.stateSequence.Add(initialState);
		}

		public void addState(Vector<double> s)
		{
			stateSequence.Add(s);
		}

		public void addAction(Action a)
		{
			actionSequence.Add(a);
		}

		public void addReward(double r)
		{
			rewardSequence.Add(r);
		}

		public void transition(Action usingAction, Vector<double> nextState, double r)
		{
			stateSequence.Add(nextState);
			actionSequence.Add(usingAction);
			rewardSequence.Add(r);
		}

		public Vector<double> state(int t)
		{
			if (t >= this.stateSequence.Count)
				throw new Exception("Episode has nothing recorded for this time step " + t);
			return stateSequence[t];
		}

		public Action action(int t)
		{
			if (t >= this.actionSequence.Count)
				throw new Exception("Episode has nothing recorded for this time step " + t);
			if (t == this.actionSequence.Count)
				throw new Exception("Episode does not containt action at time step " + t +
									". Note that Episode always has a final state at one time step larger than the last" +
									"action time step");
			return actionSequence[t];
		}

		public double reward(int t)
		{
			if (t == 0)
				throw new Exception("Cannot retrun the reward received at time step 0; the first received reward occrus after the initial" +

									"state at time step 1");
			if (t > rewardSequence.Count)
				throw new Exception("There are only" + this.rewardSequence.Count + " rewards recorded; cannot return reward for time step " + t);
			return rewardSequence[t - 1];
		}

		public int numTimeStep() { return this.stateSequence.Count; }

		public int maxTimeStep() { return this.stateSequence.Count - 1; }

		public int numActions() { return this.actionSequence.Count; }

		public double discountedRetrun(double discountFactor)
		{
			double discount = 1;
			double sum = 0;
			foreach (double r in rewardSequence)
			{
				sum += discount * r;
				discount *= discountFactor;
			}
			return sum;
		}

		public string actionString() { return this.actionString("; "); }
		public string actionString(string delimiter)
		{
			StringBuilder buf = new StringBuilder();
			bool first = true;
			foreach (Action ga in actionSequence)
			{
				if (!first)
				{
					buf.Append(delimiter);
					first = false;
				}
			}
			return buf.ToString();
		}

		public static void WriteEpisodes(List<Episode> episodes, string directoryPath, string baseFileName)
		{
			for (int i = 0; i < episodes.Count; i++)
			{
				Episode ea = episodes[i];
				ea.write(baseFileName + i);


			}
		}

		public void write(string path)
		{
			if (!path.EndsWith(".json", StringComparison.Ordinal))
			{
				path = path + ".json";
			}
			try
			{
				String str = this.serialize();
				DependencyService.Get<IFileHelper>().WriteAllText(path, str);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}

		public string serialize()
		{
			string json = JsonConvert.SerializeObject(this);
			return json;
		}

		//public static List<Episode> readEpisodes(string baseFileName)
		//{
		//	List<Episode> eas = new List<Episode>();
		//	string ext = ".json";
		//	Directory.
		//	for (int i = 0; i < )
		//}

		//public static Episode read(string fileName)
		//{
		//	string fcont = null;
		//}

		public Episode copy()
		{
			Episode ep = new Episode();
			ep.stateSequence = new List<Vector<double>>(this.stateSequence);
			ep.actionSequence = new List<Action>(this.actionSequence);
			ep.rewardSequence = new List<double>(this.rewardSequence);
			return ep;
		}
	}
}
