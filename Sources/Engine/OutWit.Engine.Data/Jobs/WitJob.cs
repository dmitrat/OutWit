using System.Linq;
using OutWit.Engine.Data.Attributes;
using OutWit.Engine.Interfaces;

namespace OutWit.Engine.Data.Jobs
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