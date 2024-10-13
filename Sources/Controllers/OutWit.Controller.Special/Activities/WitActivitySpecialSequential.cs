using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Jobs;

namespace OutWit.Controller.Special.Activities
{
    [Activity("Sequence")]
    public class WitActivitySpecialSequential : WitJobBase
    {
        public override int StagesCount => Activities.Count;
    }
}
