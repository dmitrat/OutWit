using Microsoft.Extensions.Logging;
using OutWit.Common.Interfaces;
using OutWit.WitEngine.Shared.Interfaces;
using OutWit.WitEngine.Shared.Utils;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Shared.Activities
{
    public abstract class WitActivityAdapterVoid<TActivity> : WitActivityAdapterSingleOperator<TActivity>
        where TActivity : IWitActivity
    {
        #region Constructors

        protected WitActivityAdapterVoid(IWitProcessingManager processingManager, ILogger logger, IWitResources resources) :
            base(processingManager, logger, resources)
        {
        } 

        #endregion

        #region IWMActivityAdapter

        protected override string Serialize(TActivity activity, string prefix)
        {
            return $"{StringOperations.WriteOperatorHeader(ActivityType, SerializeParameters(activity), prefix)};";
        }

        #endregion


    }
}
