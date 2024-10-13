using OutWit.Common.Prism.Interfaces;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OutWit.Demo.WitEngine.ViewModels;

namespace OutWit.Demo.WitEngine.Views
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    [ViewSortHint("01")]
    public partial class Navigation : UserControl, INavigationControl
    {
        #region Constructors

        public Navigation()
        {
            InitializeComponent();
            DataContext = ApplicationViewModel.Instance;
        }

        #endregion

        #region INavigationControl

        public void Deselect()
        {
            Context.IsSelected = false;
            Context.IsVisible = false;
        }

        public void Collapse()
        {
            Context.IsVisible = false;
        }

        public void Expand()
        {
            Context.IsVisible = true;
        }

        #endregion

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Context.Navigate(navigationContext.Parameters);
            Context.IsSelected = true;
            Context.IsVisible = true;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        #endregion

        #region Properties

        public INavigationContext Context => ApplicationViewModel.Instance.NavigationVm;

        #endregion
    }
}
