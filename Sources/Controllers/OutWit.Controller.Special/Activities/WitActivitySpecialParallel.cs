using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Jobs;

namespace OutWit.Controller.Special.Activities
{
    [Activity("Parallel")]
    public class WitActivitySpecialParallel : WitJobBase
    {
        public override int StagesCount => 1;
    }
}
