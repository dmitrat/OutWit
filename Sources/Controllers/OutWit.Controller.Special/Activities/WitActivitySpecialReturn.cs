using OutWit.Engine.Data.Activities;
using OutWit.Engine.Data.Attributes;

namespace OutWit.Controller.Special.Activities
{
    [Activity("Return")]
    public class WitActivitySpecialReturn : WitActivity
    {
        public WitActivitySpecialReturn(params string[] value)
        {
            Value = value;
        }

        public string[] Value { get;}
    }
}
