using OutWit.Engine.Data.Activities;
using OutWit.Engine.Data.Attributes;

namespace OutWit.Controller.Special.Activities
{
    [Activity("Path")]
    public class WitActivitySpecialPath : WitActivity
    {
        public WitActivitySpecialPath(string @return, params string[] pathParts)
        {
            Return = @return;
            PathParts = pathParts;
        }
        
        public string Return { get;}
        
        public string[] PathParts { get;}
    }
}
