using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OutWit.WitEngine.Interfaces;

namespace OutWit.WitEngine.Data.Activities
{
    public class WitActivityCollection : IReadOnlyList<IWitActivity>
    {
        #region Fields

        private readonly List<IWitActivity> m_items = new List<IWitActivity>();

        #endregion

        #region Constructors

        public WitActivityCollection()
        {
        }

        #endregion

        #region Functions

        public void Add(IWitActivity action)
        {
            m_items.Add(action);
        }

        #endregion

        #region IEnumerable

        public IEnumerator<IWitActivity> GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Properties

        public IWitActivity this[int index] => m_items[index];

        public int StagesCount => m_items.Select(activity => activity.StagesCount).Sum();

        public int Count => m_items.Count;

        #endregion
    }
}
