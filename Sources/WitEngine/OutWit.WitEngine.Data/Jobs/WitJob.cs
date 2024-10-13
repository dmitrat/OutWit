using System.Linq;
using OutWit.WitEngine.Data.Attributes;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Data.Jobs
{
    public abstract class WitJob : WitJobBase, IWitNamed
    {
        #region Constructors

        private WitJob() : this("")
        {

        }

        protected WitJob(string name)
        {
            Name = name;
        }

        #endregion

        #region Properties

        public string Name { get;}

        public override string Type => OperatorAttribute.GetOperatorType<JobAttribute>(GetType());

        public override int StagesCount => Activities.Sum(x => x.StagesCount);

        #endregion
    }
}