using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Jobs;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Jobs.Jobs
{
    [Job("Job(String)")]
    public class WitJobString : WitJobOneParameter<string>
    {
        #region Constructors
        public WitJobString(string name, WitVariable<string> parameter) : base(name, parameter)
        {
        }

        #endregion
    }
}