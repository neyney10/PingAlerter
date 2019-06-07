using PingAlerter.Common;
using PingAlerter.IO.FileSystem;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MainWindow;
using PingAlerter.Other.MonitorConfig;
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

        MonitorTabControlViewModel monitorTabControlViewModel;

        public MonitorTabControlView()
        {
            InitializeComponent();

            InitViewModels();
        }


        public void InitViewModels()
        {

            monitorTabControlViewModel = new MonitorTabControlViewModel();

            // General window
            this.DataContext = monitorTabControlViewModel;

            tab_logs.DataContext        = monitorTabControlViewModel.logViewModel;
            tab_monitor.DataContext     = monitorTabControlViewModel.monitorTabViewModel;
            tab_settings.DataContext    = monitorTabControlViewModel.monitorConfigViewModel;
            
            // Experimental tab
            // Btn_experimental.DataContext = experimentalViewModel;


        }



        public class ExperimentalViewModel
        {

            public ICommand Btn_experimental_OnClick { get; set; }

            public ExperimentalViewModel()
            {
                this.Btn_experimental_OnClick = new ExperimentalCommand();
            }

            public class ExperimentalCommand : ICommand
            {
                public event EventHandler CanExecuteChanged;

                public bool CanExecute(object parameter)
                {
                    return true;
                }

                public void Execute(object parameter)
                {
                    new Window().Show();
                }
            }
        }
    }
}
