using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace Microsoft.Band.Portable.iML
{
	public interface LearningAgent
	{
		Episode runLearningEpisode(Environment env, int maxSteps);
		List<QValue> qValues(Vector<double> s);
		double qValue(Vector<double> s, Action a);
		QValue storedQ(Vector<double> s, Action a);
		double value(Vector<double> s);
	}
}
