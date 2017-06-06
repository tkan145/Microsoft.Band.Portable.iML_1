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

        RelayCommand<object> saveCommand;
        public RelayCommand<object> SaveCommand =>
        saveCommand ?? (saveCommand = new RelayCommand<object>(async (chart) => await ExecuteSaveCommandAsync(chart)));
        public async Task ExecuteSaveCommandAsync(object chart)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var sfChart = chart as SfChart;
                try
                {
                    sfChart.SaveAsImage("Chart.png");
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
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

        private QLearning qLearning = null;         // Q-Learning algorithm
        private Sarsa sarsa = null;                 // Sarsa Algorithm

        //private int numAction = env.getActionSpace();
        private int actionIndex;

        private LearningAgent learner = null;

        Vector<double> currentState;
        Vector<double> nextState;

        public async Task TrainQAgent(Environment env, iMLModel Agent)
        {
            qLearning = null;
            sarsa = null;
            switch (Agent.Algorithm)
            {
                case "Q-Learning":
                    // create new QLearning algorithm's instance
                    learner = new QLearning();
                    break;
                case "SARSA":
                    // create new Sarsa algorithm's instance
                    break;
                case "LAMDA":
                    // Create new Lambda algorithm's instance
                    break;
                default:
                    learner = new QLearning();
                    break;
            }
            await StartTraining(env, learner);
        }



        private async Task StartTraining(Environment env, LearningAgent agent)
        {
            // Keep track of useful statistics

            // THe policy we are following

            while (epochsDone < epochsWaiting) // also not in goal state
            {
                if (!isTraining)
                    break;
                // Print out which episode we're on, useful for debugging.
                if ((epochsDone + 1) % 10 == 0)
                    Debug.WriteLine("\rEpisode {0}/{1}", epochsDone, epochsWaiting);

                // Reset the environement and pick the first action
                await env.Reset();

                Debug.WriteLine("Start eposch {0}", epochsDone);

                // One step in the environment
                // total reward = 0;
                var episode = learner.runOneEpisode();

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
