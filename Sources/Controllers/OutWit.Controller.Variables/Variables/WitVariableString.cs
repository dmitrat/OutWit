using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Variables;

namespace OutWit.Controller.Variables.Variables
{
    [Variable("String")]
    public class WitVariableString : WitVariable<string>
    {
        public WitVariableString(string name) : base(name)
        {
        }

        public WitVariableString(string name, string value) : base(name, value)
        {
        }
    }
}
