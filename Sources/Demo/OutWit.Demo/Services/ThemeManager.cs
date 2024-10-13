using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutWit.Themes.Interfaces;

namespace OutWit.Demo.Services
{
    public class ThemeManager : IThemeContainerBase
    {
        #region Events

        public event CurrentThemeChangedEventHandler CurrentThemeChanged = delegate { };

        #endregion

        #region Constructors

        public ThemeManager()
        {
            InitDefaults();
            InitEvents();
        }

        #endregion

        #region Initialziation

        private void InitDefaults()
        {

            Themes.Selector.Get.Reload(ServiceLocator.Get.ResourcesManager);
        }

        private void InitEvents()
        {
            Themes.Selector.Get.ThemeContainer.CurrentThemeChanged += OnCurrentThemeChanged;
        }


        #endregion

        #region Event Hadlers

        private void OnCurrentThemeChanged(ITheme currentTheme)
        {
            CurrentThemeChanged(currentTheme);
        }

        #endregion

        #region Properties

        public ITheme Current => Themes.Selector.Get.ThemeContainer.Current;

        #endregion

    }
}
