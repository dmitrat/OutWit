using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Jobs;

namespace OutWit.Controller.Special.Activities
{
    [Activity("Loop")]
    public class WitActivitySpecialLoop : WitJobBase
    {
        public WitActivitySpecialLoop(int iterationsCount)
        {
            IterationsCount = iterationsCount;
        }
        
        public int IterationsCount { get; }

        public override int StagesCount => base.Activities.StagesCount * IterationsCount;
    }
}
