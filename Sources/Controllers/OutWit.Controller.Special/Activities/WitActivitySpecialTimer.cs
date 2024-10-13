using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Jobs;

namespace OutWit.Controller.Special.Activities
{
    [Activity("Timer")]
    public class WitActivitySpecialTimer : WitJobBase
    {
        public WitActivitySpecialTimer (int interval, long timeout = 6000000)
        {
            Interval = interval;
            Timeout = timeout;
        }

        public int Interval { get; }
        public long Timeout { get; }

        public override int StagesCount => Activities.Count;
    }
}
