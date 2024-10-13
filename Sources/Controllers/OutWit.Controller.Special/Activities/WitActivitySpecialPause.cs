using OutWit.Engine.Data.Activities;
using OutWit.Engine.Data.Attributes;

namespace OutWit.Controller.Special.Activities
{
    [Activity("Pause")]
    public class WitActivitySpecialPause : WitActivity
    {
        public WitActivitySpecialPause(string message, long timeout = 600000)
        {
            Message = message;
            Timeout = timeout;
        }
        
        public string Message { get;}
        public long Timeout { get; }
    }
}
