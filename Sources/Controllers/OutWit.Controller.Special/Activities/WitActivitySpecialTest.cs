using OutWit.Engine.Data.Activities;
using OutWit.Engine.Data.Attributes;

namespace OutWit.Controller.Special.Activities
{
    [Activity("Test")]
    public class WitActivitySpecialTest : WitActivity
    {
        public WitActivitySpecialTest(string @return, string message, bool throwException = false)
        {
            Return = @return;
            Message = message;
            ThrowException = throwException;
        }
        
        public string Return { get;}
        
        public string Message { get;}

        public bool ThrowException { get;}
    }
}
