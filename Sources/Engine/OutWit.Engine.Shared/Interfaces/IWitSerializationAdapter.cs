using OutWit.Engine.Interfaces;

namespace OutWit.Engine.Shared.Interfaces
{
    public interface IWitSerializationAdapter
    {
        string Serialize(IWitOperator activity, string prefix);

        void Deserialize(string activityStr, IWitJob job);

        IWitOperator Clone(IWitOperator obj);
    }
}
