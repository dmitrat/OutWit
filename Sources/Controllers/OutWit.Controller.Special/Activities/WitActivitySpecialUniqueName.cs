using OutWit.Engine.Data.Activities;
using OutWit.Engine.Data.Attributes;

namespace OutWit.Controller.Special.Activities
{
    [Activity("UniqueName")]
    public class WitActivitySpecialUniqueName : WitActivity
    {
        public WitActivitySpecialUniqueName(string @return, string extension="")
        {
            Return = @return;
	        Extension = extension;
        }
        
        public string Return { get;}
        
        public string Extension { get;}
    }
}
