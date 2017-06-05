using System;
namespace Microsoft.Band.Portable.iML
{
	public interface IExplorationPolicy
	{
		int ChooseAction(double[] actionEstimates);
	}
}
