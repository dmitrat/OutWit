using OutWit.Engine.Data.Activities;
using OutWit.Engine.Data.Attributes;

namespace OutWit.Controller.Special.Activities
{
    [Activity("ConcatString")]
    public class WitActivitySpecialConcatString : WitActivity
    {
        public WitActivitySpecialConcatString(string @return, params string[] stringParts)
        {
            Return = @return;
            StringParts = stringParts;
        }
        
        public string Return { get;}
        
        public string[] StringParts { get;}
    }
}
