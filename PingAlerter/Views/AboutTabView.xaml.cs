using PingAlerter.ViewModels;
using System.Windows.Controls;
using System;
using System.Diagnostics;

namespace PingAlerter.Views
{
    /// <summary>
    /// Interaction logic for AboutTabView.xaml
    /// </summary>
    public partial class AboutTabView : UserControl
    {
        public AboutViewModel AboutViewModel
        {
            set { this.DataContext = value; }
            get { return this.DataContext as AboutViewModel; }
        }

        public AboutTabView()
        {
            InitializeComponent();
        }

    }
}
