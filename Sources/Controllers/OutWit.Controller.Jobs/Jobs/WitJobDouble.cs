using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Jobs;
using OutWit.Engine.Data.Variables;

namespace OutWit.Controller.Jobs.Jobs
{
    [Job("Job(Double)")]
    public class WitJobDouble : WitJobOneParameter<double>
    {
        #region Constructors
        
        public WitJobDouble(string name, WitVariable<double> parameter) : base(name , parameter)
        {
        }

        #endregion
    }
}