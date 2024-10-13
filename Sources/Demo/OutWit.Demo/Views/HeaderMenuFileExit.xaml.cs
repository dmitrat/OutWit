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
using OutWit.Demo.ViewModels;
using Prism.Navigation.Regions;

namespace OutWit.Demo.Views
{
    /// <summary>
    /// Interaction logic for HeaderMenuFileExit.xaml
    /// </summary>
    [ViewSortHint("996")]
    public partial class HeaderMenuFileExit : MenuItem
    {
        public HeaderMenuFileExit()
        {
            InitializeComponent();
        }
    }
}
