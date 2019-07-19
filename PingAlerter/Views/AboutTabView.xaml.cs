using PingAlerter.ViewModels;
using System.Windows.Controls;
using System;


namespace PingAlerter.Views
{
    /// <summary>
    /// Interaction logic for AboutTabView.xaml
    /// </summary>
    public partial class AboutTabView : UserControl
    {
        public AboutViewModel AboutViewModel { get; set; }

        public AboutTabView()
        {
            InitializeComponent();
        }

    }
}
