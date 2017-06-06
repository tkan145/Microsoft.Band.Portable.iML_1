using System;
namespace Microsoft.Band.Portable.iML.RL
{
    [Flags]
    public enum Algorithm
    {
        Q_LEARNING = 1,
        SARSA = 2,
        LAMBDA = 3,
    }
}
