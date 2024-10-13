using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutWit.Demo.WitEngine.ViewModels
{
    public class ApplicationViewModel : BindableBase
    {
        #region Static Fields

        private static volatile ApplicationViewModel m_instance = null;
        private static readonly object m_syncRoot = new Object();

        #endregion

        #region Constructors

        private ApplicationViewModel()
        {
            InitViewModels();
        }

        #endregion

        #region Functions

        private void InitViewModels()
        {
            NavigationVm = new NavigationViewModel(this);
            JobEditorVm = new JobEditorViewModel(this);
        }

        #endregion

        #region Properties

        public NavigationViewModel NavigationVm { get; private set; }
        public JobEditorViewModel JobEditorVm { get; private set; }

        #endregion

        #region Static Properties

        public static ApplicationViewModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new ApplicationViewModel();
                    }
                }

                return m_instance;
            }
        }

        #endregion
    }
}
