using System;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using Xamarin.Forms;
using FormsToolkit;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Band.Portable.iML
{
	public class MicrosoftBandEnvironment : Environment
	{
		MatrixBuilder<double> M = Matrix<double>.Build;
		Matrix<double> states;
		int currentStateIndex;
		double reward;
		List<Action> a;
		bool isReset;
		double rewardMax;
		double rewardMin;


		readonly string logRewardPath = "reward.txt";
		readonly string logQValuePath = "qValue.txt";

		public enum Action
		{
			MovingUp,
			MovingDown,
			MovingLeft,
			MovingRight,
		}

		public MicrosoftBandEnvironment(double maxReward, double minReward)
		{
			Init(maxReward, minReward);
		}
		//public void SetAction(List<Action> a) { }
		//public List<Action> GetAction() { return null; }
		public void Init(double rewardMax, double rewardMin)
		{
			// 50 rows and 3 colunm(x,y,z) and init with 0
			states = M.Dense(50, 3, 0);
			currentStateIndex = 0;
			a = new List<Action>();
			isReset = false;
			this.rewardMax = rewardMax;
			this.rewardMin = rewardMin;
		}

		public int getActionSpace()
		{
			return Enum.GetNames(typeof(Action)).Length;
		}
		public async Task Reset()
		{
			Debug.WriteLine("Inside band");
			// Start record data
			await App.current.Bands.bandData.StartCollectAccelerometerDataAsync();
			Debug.WriteLine("Done recording");
			// Load data into our State matrix
			InitStates(this.states);
		}

		public void SetAction(List<Action> a)
		{
			this.a = a;
		}

		public void GetAction()
		{
			//if (this.a.Count > 0)
			//	return a;
			//else
			//return null;
			//throw new Exception("No action");
		}

		public bool isInTerminalState()
		{
			// If we are at the end of the window
			if (currentStateIndex == states.RowCount - 1)
				return true;
			else
			{
				return false;
			}
		}

		public double getReward(string Action)
		{
			bool parsed = false;
			string message = "Are you " + Action;
			MessagingService.Current.SendMessage<MessagingServiceQuestion>(MessageKeys.Question, new MessagingServiceQuestion
			{
				Title = "Choosen Action",
				Question = message,
				Positive = "Yes",
				Negative = "No",
				OnCompleted = (result) =>
					{
						if (result)
						{
							this.reward = rewardMax;
						}
					}

			});
			this.reward = rewardMin;
			return this.reward;
			//Debug.WriteLine("Send Message " + Action);
			//MessagingService.Current.Subscribe(MessageKeys.Reward, (arg1) =>
			// {
			//	 Debug.WriteLine("Send Message");
			//	 parsed = double.TryParse(arg1.ToString(), out this.reward);
			// });
			//if (parsed)
			//	return this.reward;
			//else
			//{
			//	isReset = true;
			//	currentStateIndex = 0;
			//	return 0;
			//}
			return 0;
		}

		public async Task<double> GetReward()
		{
			await Task.Delay(1000);
			return 0;
		}

		public bool isResetEnv()
		{
			return this.isReset;
		}

		public void setResetEnv(bool reset)
		{
			this.isReset = reset;
		}

		public Vector<double> GetCurrentObservation()
		{
			return states.Row(currentStateIndex);
		}

		public Vector<double> GetNextState()
		{
			return states.Row(++currentStateIndex);
		}

		public void InitStates(Matrix<double> states)
		{
			states.Clear();
			Matrix<double> temp;
			// Check if we the file exist then load it to matrix
			bool fileExist = DependencyService.Get<IFileHelper>().Exist("test1.csv");
			if (fileExist)
			{
				var x = DependencyService.Get<IFileHelper>().LoadCsv("test1.csv");
				temp = M.DenseOfArray(x);

				// Get window size
				int windowSize = (int)(temp.RowCount / states.RowCount);

				// Keep track of current states matrix index
				int currentIndex = 0;

				// Get average of window
				for (int i = 0; i < temp.RowCount; i = i + windowSize)
				{
					// Set each row of states to average of window
					states.SetRow(currentIndex, temp.SubMatrix(i, windowSize, 0, 3).ColumnSums() / windowSize);
					currentIndex++;
				}
			}
		}
	}
}
