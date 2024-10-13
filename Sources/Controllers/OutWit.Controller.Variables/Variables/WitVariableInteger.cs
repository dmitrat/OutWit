using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Variables;

namespace OutWit.Controller.Variables.Variables
{
    [Variable("Int")]
    public class WitVariableInteger : WitVariable<int>
    {
        public WitVariableInteger(string name) : base(name)
        {
        }

        public WitVariableInteger(string name, int value) : base(name, value)
        {
        }
    }
}
