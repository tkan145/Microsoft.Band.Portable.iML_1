using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Microsoft.Band.Portable.iML
{
	public interface Environment
	{
		void Init(double max, double min);
		Task Reset();
		bool isInTerminalState();
		double getReward(string Action);
		Vector<double> GetCurrentObservation();
		Vector<double> GetNextState();
		//void SetAction(List<Action> a);
		//void GetAction();
		bool isResetEnv();
		void setResetEnv(bool s);
		int getActionSpace();
	}
}
