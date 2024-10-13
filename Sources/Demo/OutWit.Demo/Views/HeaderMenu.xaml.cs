using OutWit.Common.MVVM.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OutWit.Common.MVVM.Aspects;

namespace OutWit.Demo.Views
{
    /// <summary>
    /// Interaction logic for HeaderMenu.xaml
    /// </summary>
    public partial class HeaderMenu : UserControl
    {
        #region DependencyProperties

        public static readonly DependencyProperty IsDebugProperty = BindingUtils.Register<HeaderMenu, bool>(nameof(IsDebug));

        #endregion

        #region Constructors

        public HeaderMenu()
        {
#if DEBUG
            IsDebug = true;
#endif

            InitializeComponent();
        } 

        #endregion


        #region Properties

        [Bindable]
        public bool IsDebug { get; set; }

        #endregion
    }
}
