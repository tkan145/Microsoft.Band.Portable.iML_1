using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using FormsToolkit;
using GalaSoft.MvvmLight.Command;
using Microsoft.Band.Portable.iML.DataStore.Abstractions;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using MathNet.Numerics.LinearAlgebra;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;
using System.Threading;
namespace Microsoft.Band.Portable.iML
{
	public class AgentDetailsViewModel : BaseViewModel
	{
		// Environment which agent will interact with. In this case will be Microsoft Band
		public static Environment env;
		VectorBuilder<double> V = Vector<double>.Build;
		public ObservableRangeCollection<ChartDataPoint> XData
		{ get; set; }
		public AgentDetailsViewModel(INavigation navigation, iMLModel model) : base(navigation)
		{
			Agent = model;
			isTraining = false;
			env = new MicrosoftBandEnvironment(Agent.MaxReward, Agent.MinReward);

			XData = new ObservableRangeCollection<ChartDataPoint>();




		}

		#region Message Service
		public void SubscribeMessage()
		{

		}

		public void UnsubscribeMessage()
		{
			MessagingService.Current.Unsubscribe(MessageKeys.FeedbackActon);
			Debug.WriteLine("Unsubscribe to Action Message");
		}

		#endregion

		#region Commands
		RelayCommand deleteCommand;
		public RelayCommand DeleteCommand =>
			deleteCommand ?? (deleteCommand = new RelayCommand(async () => await ExecuteDeleteCommandAsync()));
		public async Task ExecuteDeleteCommandAsync()
		{
			if (IsBusy)
				return;

			try
			{
				IsBusy = true;
				await StoreManager.ModelStore.RemoveAsync(Agent);
				await Navigation.PopToRootAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				IsBusy = false;
			}
		}


		RelayCommand loadLogCommand;
		public RelayCommand LoadLogCommand =>
				loadLogCommand ?? (loadLogCommand = new RelayCommand(async () => await ExecuteLoadLogCommandAsync()));
		public async Task ExecuteLoadLogCommandAsync()
		{

			if (IsBusy)
				return;

			try
			{
				IsBusy = true;
				// Load all epochs log here
				//Agent.Logs = await StoreManager.LogStore.Log(Session);
			}
			catch (Exception ex)
			{
				MessagingService.Current.SendMessage(MessageKeys.Error, ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		RelayCommand loadPreTrainedAgentCommand;
		public RelayCommand LoadPreTrainedAgentCommand =>
			loadPreTrainedAgentCommand ?? (loadPreTrainedAgentCommand = new RelayCommand(async () => await ExecuteLoadPreTrainedAgentCommandAsync()));
		public async Task ExecuteLoadPreTrainedAgentCommandAsync()
		{

			if (IsBusy)
				return;

			try
			{
				IsBusy = true;
				// Check if we have file
				// If yes then load the file.
				// Load json file here




			}
			catch (Exception ex)
			{
				MessagingService.Current.SendMessage(MessageKeys.Error, ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		RelayCommand trainCommand;
		public RelayCommand TrainCommand =>
			trainCommand ?? (trainCommand = new RelayCommand(async () => await ExecuteTrainCommandAsync()));
		public async Task ExecuteTrainCommandAsync()
		{
			if (IsBusy)
				return;

			try
			{
				IsBusy = true;
				Stopwatch watch = new Stopwatch();
				watch.Start();
				if (App.current.Bands.bandClient == null)
				{
					MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
					{
						Title = "No Band Selected",
						Message = "Please connect your band before training.",
						Cancel = "OK"
					});
					return;
				}
				watch.Stop();
				var esplap = watch.ElapsedMilliseconds;
				Debug.WriteLine("Finshed in :{0}", esplap);
				if (isTraining)
				{
					isTraining = false;
					TrainButtonText = "Train this agent";



				}
				else
				{
					TrainButtonText = "Stop training";
					isTraining = true;
					await TrainQAgent(env, Agent).ConfigureAwait(false);

				}
			}
			catch (Exception ex)
			{
				MessagingService.Current.SendMessage(MessageKeys.Error, ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		//RelayCommand<string> rewardCommand;
		//public RelayCommand<string> RewardCommand =>
		//rewardCommand ?? (rewardCommand = new RelayCommand<string>(async (key) => await ExecuteRewardCommandAsync(key)));
		//public async Task ExecuteRewardCommandAsync(string key)
		//{
		//	if (IsBusy)
		//		return;

		//	try
		//	{
		//		IsBusy = true;
		//		if (key == "true")
		//		{
		//			//this.reward = Agent.MaxReward;
		//			MessagingService.Current.SendMessage(MessageKeys.Reward, Agent.MaxReward.ToString());
		//		}
		//		else if (key == "false")
		//		{
		//			//this.reward = Agent.MinReward;
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessagingService.Current.SendMessage(MessageKeys.Reward, Agent.MinReward.ToString());
		//	}
		//	finally
		//	{
		//		IsBusy = false;
		//	}
		//}

		//RelayCommand reduceRewardCommand;
		//public RelayCommand ReduceRewardCommand =>
		//	reduceRewardCommand ?? (reduceRewardCommand = new RelayCommand(async () => await ExecuteReduceCommandAsync()));
		//public async Task ExecuteReduceCommandAsync()
		//{
		//	if (IsBusy)
		//		return;

		//	try
		//	{
		//		IsBusy = true;
		//		if (inter >= 50)
		//		{
		//			ActionResult = "End of interation";
		//			await App.current.Bands.bandData.StartCollectAccelerometerDataAsync();
		//			Agent.Eposch++;
		//		}

		//		await Task.Delay(3000);
		//		var values = Enum.GetValues(typeof(Action));
		//		Random random = new Random();
		//		Action result = (Action)values.GetValue(random.Next(values.Length));
		//		ActionResult = result.ToString();
		//		inter++;
		//		reward = (reward - 1) / inter;
		//		XData.Add(new ChartDataPoint(Agent.Eposch, reward));
		//	}
		//	catch (Exception ex)
		//	{
		//		MessagingService.Current.SendMessage(MessageKeys.Error, ex);
		//	}
		//	finally
		//	{
		//		IsBusy = false;
		//	}
		//}
		#endregion
		#region QLearner
		//learning settings
		int learningIterations = 100;
		int epochsWaiting = 100;
		int epochsDone = 0;
		private double explorationRate;
		private double learningRate;
		private double minExploreRate = 0.01;
		private double minLeaningRate = 0.1;

		private bool DebugMode = true;

		//// Q-Learning algorithm
		private QLearning qLearning = null;

		//// Sarsa Algorithm
		private Sarsa sarsa = null;

		//private int numAction = env.getActionSpace();
		private int actionIndex;

		Vector<double> currentState;
		Vector<double> nextState;

		public async Task TrainQAgent(Environment env, iMLModel agent)
		{
			Debug.WriteLine("Inside train agent");
			AgentInit();
			qLearning = null;
			sarsa = null;
			switch (Agent.Algorithm)
			{
				case "Q-Learning":
					// create new QLearning algorithm's instance
					qLearning = new QLearning(1000, 4, new TabuSearchExploration(4, new EpsilonGreedyExploration(explorationRate)));
					await StartQLearning(env, qLearning);
					break;
				case "SARSA":
					// create new Sarsa algorithm's instance
					sarsa = new Sarsa(256, 4, new TabuSearchExploration(4, new EpsilonGreedyExploration(explorationRate)));
					//		await StartSarsaLearning(env, sarsa);
					break;
				default:
					qLearning = new QLearning(1000, 4, new TabuSearchExploration(4, new EpsilonGreedyExploration(explorationRate)));
					await StartQLearning(env, qLearning);
					break;
			}
			//if (Agent.Algorithm == "Q-LEARNING")
			//{

			//}
			//else if (agent.Algorithm == "SARSA")
			//{
			//	// create new Sarsa algorithm's instance
			//	sarsa = new Sarsa(256, 4, new TabuSearchExploration(4, new EpsilonGreedyExploration(explorationRate)));
			//	//		await StartSarsaLearning(env, sarsa);
			//}

		}

		private async Task StartQLearning(Environment env, QLearning qLearning)
		{
			int iteration = 0;
			TabuSearchExploration tabuPolicy = (TabuSearchExploration)qLearning.ExplorationPolicy;
			EpsilonGreedyExploration explorationPolicy = (EpsilonGreedyExploration)tabuPolicy.BasePolicy;

			await Task.Delay(100);
			Debug.WriteLine("isTraing " + isTraining);
			Debug.WriteLine("done " + epochsDone);
			Debug.WriteLine("waiting " + epochsWaiting);
			while (epochsDone < epochsWaiting) // also not in goal state
			{
				if (!isTraining)
					break;
				// set exploration rate for this iteration
				explorationPolicy.Epsilon = explorationRate - ((double)iteration / learningIterations) * explorationRate;
				// set learning rate for this iteration
				qLearning.LearningRate = learningRate - ((double)iteration / learningIterations) * learningRate;
				// clear tabu list
				tabuPolicy.ResetTabuList();
				Debug.WriteLine("In here");
				// Reset environement for new eposch
				await env.Reset();
				Debug.WriteLine("Start eposch {0}", epochsDone);
				//steps performed by agent to get to the goal

				int steps = 0;

				// Get initial state;
				currentState = env.GetCurrentObservation();

				while (!isTraining && iteration < learningIterations && env.isInTerminalState())
				{
					Debug.WriteLine("Start {0} iteration", iteration);
					steps++;
					// get the action for this state
					//string action = qLearning.GetAction(currentState);
				}
			}
			// get current state
		}







		//		// Reset environement for new eposch
		//		await env.Reset();

		//		// steps performed by agent to get to the goal
		//		int steps = 0;

		//		// Get initial state;
		//		//currentState = env.GetCurrentObservation();


		//		while (!isTraining && iteration < learningIterations && env.isInTerminalState())
		//		{
		//			steps++;

		//			// get the action for this state
		//			//string action = qLearning.GetAction(currentState);

		//			// Execute an action - get next observation, reward, isDone?
		//			//double reward = env.getReward(action);

		//			// get agent's next state
		//			//nextState = env.GetNextState();

		//			// do learning of the agent - update his Q-function
		//			//qLearning.UpdateState(currentState, action, reward, nextState);

		//			// set tabu action
		//			//tabuPolicy.SetTabuAction((action + 2) % 4, 1);
		//			//currentState = nextState;

		//			// Print debug data
		//			if (DebugMode)
		//			{
		//				Debug.WriteLine("\nEpisode = {0}\t", epochsDone);
		//				Debug.WriteLine("Interation = {0}\n", iteration);
		//				//Debug.WriteLine("Action: {0}\n", action.ToString());
		//				//Debug.WriteLine("State: {0}\n", currentState.ToString());
		//				//Debug.WriteLine("Reward: {0}\n", reward);
		//				Debug.WriteLine("Best Q: {0}\n", 0);
		//				Debug.WriteLine("Explore rate: {0}\n", explorationRate);
		//				Debug.WriteLine("Learning rate: {0}\n", learningRate);
		//				Debug.WriteLine("\n");
		//			}

		//			if (env.isInTerminalState())
		//			{
		//				Debug.WriteLine("Episode {0} finished after {1} time steps", epochsDone, iteration);
		//				break;
		//			}
		//		}
		//		epochsDone++;
		//	}
		//}

		//private async Task StartSarsaLearning(Environment env, Sarsa qLearning)
		//{
		//	int iteration = 0;
		//	// get current state
		//	Vector<double> currentState;
		//	Vector<double> nextState;

		//	TabuSearchExploration tabuPolicy = (TabuSearchExploration)sarsa.ExplorationPolicy;
		//	EpsilonGreedyExploration explorationPolicy = (EpsilonGreedyExploration)tabuPolicy.BasePolicy;

		//	await env.Reset();

		//	//List<Episode> episodes = new List<Episode>();
		//	while (!isTraining && epochsDone < epochsWaiting) // also not in goal state
		//	{
		//		// set exploration rate for this iteration
		//		explorationPolicy.Epsilon = explorationRate - ((double)iteration / learningIterations) * explorationRate;
		//		// set learning rate for this iteration
		//		qLearning.LearningRate = learningRate - ((double)iteration / learningIterations) * learningRate;
		//		// clear tabu list
		//		tabuPolicy.ResetTabuList();

		//		// Reset environement for new eposch
		//		await env.Reset();

		//		// steps performed by agent to get to the goal
		//		int steps = 1;

		//		// previous state and action
		//		Vector<double> previousState = env.GetCurrentObservation();
		//		//int previousAction = sarsa.GetAction(previousState);

		//		while (!isTraining && iteration < learningIterations && env.isInTerminalState())
		//		{
		//			steps++;

		//			// Get current state;
		//			currentState = env.GetCurrentObservation();

		//			// get the action for this state
		//			//string action = qLearning.GetAction(currentState);

		//			//double reward = env.getReward(action);

		//			// get agent's next state
		//			nextState = env.GetNextState();

		//			// do learning of the agent - update his Q-function
		//			//qLearning.UpdateState(currentState, action, reward, nextState);

		//			// set tabu action
		//			//tabuPolicy.SetTabuAction((action + 2) % 4, 1);

		//		}
		//		epochsDone++;
		//	}




		//}




		private void AgentInit()
		{
			GetSettings(Agent);
		}

		private void GetSettings(iMLModel agentRL)
		{
			// explortion rate
			try
			{
				explorationRate = Math.Max(minExploreRate, Math.Min(1.0, agentRL.Gamma));
			}
			catch
			{
				explorationRate = 0.5;
			}
			// learning rate
			try
			{
				learningRate = Math.Max(minLeaningRate, Math.Min(1.0, agentRL.Alpha));
			}
			catch
			{
				learningRate = 0.5;
			}
			// learning iterations
			try
			{
				learningIterations = Math.Max(10, Math.Min(100000, agentRL.start_learn_threshold));
			}
			catch
			{
				learningIterations = 100;
			}
		}


		#endregion
		#region Property

		private bool isTraining;

		private string actionResult;
		public string ActionResult
		{
			get { return actionResult; }
			set { Set(ref actionResult, value); }
		}

		private string trainButtonText = "Train this agent";
		public string TrainButtonText
		{
			get { return trainButtonText; }
			set { Set(ref trainButtonText, value); }
		}

		private string data;
		public string Data
		{
			get { return data; }
			set { Set(ref data, value); }
		}

		iMLModel agent;
		public iMLModel Agent
		{
			get { return agent; }
			set { Set(ref agent, value); }
		}
		#endregion
	}
}
