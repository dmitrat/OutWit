using System.Linq;
using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Data.Jobs;

namespace OutWit.Controller.Jobs.Jobs
{
    [Job("Job()")]
    public class WitJobVoid : WitJob
    {
        #region Constructors

        public WitJobVoid(string name) : base(name)
        {
        }

        #endregion
    }
}