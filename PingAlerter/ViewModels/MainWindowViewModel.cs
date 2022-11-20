using PingAlerter.Common;
using PingAlerter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.ViewModels
{
    class MainWindowViewModel : BaseViewModel<Object>
    {
        private MainWindowModel mainWindowModel;
        public MonitorTabControlViewModel monitorTabControlViewModel { get; set; }
        public StatusBarViewModel statusBarViewModel { get; set; }

        public MainWindowViewModel()
        {
            this.mainWindowModel = new MainWindowModel();
            InitViewModels();
            InitClass();
        }

        private void InitViewModels()
        {
            this.monitorTabControlViewModel = new MonitorTabControlViewModel();
            this.statusBarViewModel = new StatusBarViewModel();
        }


        private void InitClass()
        {
            /*Observer<int> TabControlObserver = new Observer<int>(
                (data) =>
                {
                    statusBarViewModel.ScanCount = data;
                }
                );

            monitorTabControlViewModel.Subscribe(TabControlObserver);*/
        }

    }
}
