using PingAlerter.Common;
using PingAlerter.IO.FileSystem;
using PingAlerter.Network;
using PingAlerter.Other.Log;

using PingAlerter.Other.MonitorTab;
using PingAlerter.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PingAlerter.Views
{
    /// <summary>
    /// Interaction logic for MonitorTabControlView.xaml
    /// </summary>
    public partial class MonitorTabControlView : UserControl
    {

        public MonitorTabControlViewModel monitorTabControlViewModel { get; set; }

        public MonitorTabControlView()
        {
            InitializeComponent();

            // InitViewModels();
        }


        public void InitViewModels()
        {

            monitorTabControlViewModel = new MonitorTabControlViewModel();

            // General window
            this.DataContext = monitorTabControlViewModel;


            
            // Experimental tab
            // Btn_experimental.DataContext = experimentalViewModel;


        }


    }
}
