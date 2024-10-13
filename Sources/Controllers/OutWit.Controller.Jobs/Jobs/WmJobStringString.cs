using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Jobs;
using OutWit.Engine.Data.Variables;
using OutWit.Engine.Interfaces;

namespace OutWit.Controller.Jobs.Jobs
{
    [Job("Job(String,String)")]
    public class WmJobStringString : WitJobTwoParameters<string,string>
    {
        #region Constructors
        public WmJobStringString(string name, WitVariable<string> parameter1, WitVariable<string> parameter2) :
            base(name, parameter1, parameter2)
        {
        }

        #endregion
    }
}