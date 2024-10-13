using OutWit.Engine.Data.Activities;
using OutWit.Engine.Data.Attributes;

namespace OutWit.Controller.Special.Activities
{
    [Activity("Copy")]
    public class WitActivitySpecialCopy : WitActivity
    {
        public WitActivitySpecialCopy(string from, string to)
        {
	        From = from;
	        To = to;
        }
        
        public string From { get;}
        
        public string To { get;}
    }
}
