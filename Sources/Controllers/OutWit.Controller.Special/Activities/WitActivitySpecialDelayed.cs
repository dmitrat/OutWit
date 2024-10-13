using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Jobs;

namespace OutWit.Controller.Special.Activities
{
    [Activity("Delay")]
    public class WitActivitySpecialDelayed: WitJobBase
    {
        public WitActivitySpecialDelayed(int delay)
        {
            Delay = delay;
        }
        
        public int Delay { get;}

        public override int StagesCount => Activities.Count;
    }
}
