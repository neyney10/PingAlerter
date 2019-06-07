
using PingAlerter.Common;
using PingAlerter.IO.FileSystem;
using PingAlerter.Models;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MonitorTab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PingAlerter.ViewModels
{
    public class MonitorTabViewModel : BaseViewModel<MonitorServiceNotify>
    {
        // Model Interface and stuff
        private MonitorTabModel Model;

        private LatencyMonitorConfig Configuration { get; set; }

        public IEnumerable<string> Addresses { get { return this.Model.Addresses; } }
        public string txtb_newAddress { get { return Model.txtb_newAddress; } set { this.Model.txtb_newAddress = value; } }

        // this ViewModel interaction logic
        public StartLatencyMonitorCommand startLatencyMonitorCommand { get; set; }
        private Brush btnStartBackgroundColor;
        public Brush BtnStartBackgroundColor
        {
            get { return this.btnStartBackgroundColor; }
            set { this.btnStartBackgroundColor = value; OnPropertyChanged("BtnStartBackgroundColor"); }
        }

        private string btnStartContent;
        public string BtnStartContent
        {
            get { return this.btnStartContent; }
            set { this.btnStartContent = value; OnPropertyChanged("BtnStartContent"); }
        }


        // Temp data members
        Observer<MonitorServiceNotify> MonitorObserver;

        #region Constructors
        // constructor with default dependancy
        public MonitorTabViewModel() : this(new LatencyMonitorConfig())
        {
        }

        // With pre-defined dependancy
        public MonitorTabViewModel(LatencyMonitorConfig configuration)
        {
            this.Model = new MonitorTabModel();
            this.Configuration = configuration;

            MonitorObserver = new Observer<MonitorServiceNotify>(
                (data) => { NotifyObservers(data); });

            this.startLatencyMonitorCommand = new StartLatencyMonitorCommand(this.Model.Addresses, this);


            Init();
        }
        #endregion

        public void Init()
        {
            BtnStartContent = "Start!";
            BtnStartBackgroundColor = new LinearGradientBrush(Color.FromRgb(25, 200, 33), Color.FromRgb(0, 233, 88), 1);

        }

        public class StartLatencyMonitorCommand : ICommand
        {

            public event EventHandler CanExecuteChanged;

            private bool can_execute { get; set; }
            private bool IsChecked = false;
            private MonitorService monitorService;
            private Thread Monitor;
            private readonly IEnumerable<string> Addresses;
            private readonly MonitorTabViewModel ViewModel;



            public bool canExecute {
                get { return can_execute; }
                set { this.can_execute = value; OnCanExecuteChanged(); }
            }

            public StartLatencyMonitorCommand(IEnumerable<string> addresses, MonitorTabViewModel viewModel)
            {
                this.Addresses = addresses;
                can_execute = true;
                this.ViewModel = viewModel;
  
            }

            public bool CanExecute(object parameter)
            {
                return this.canExecute;
            }

            public void Execute(object latencyMonitorConfig)
            {

                if (IsChecked)
                {
                    IsChecked = false;
                    Monitor.Abort(); // TODO: change it to stop the thread using a boolean or somethin.

                    this.ViewModel.BtnStartBackgroundColor = new LinearGradientBrush(Color.FromRgb(25, 200, 33), Color.FromRgb(0, 233, 88), 1);
                    this.ViewModel.BtnStartContent = "Start!";

                }
                else
                {
                    IsChecked = true;
                    
                    LatencyMonitorConfig configuration = this.ViewModel.Configuration;
                    this.monitorService = new MonitorService(configuration);
                    this.monitorService.Subscribe(this.ViewModel.MonitorObserver);

                    Monitor = new Thread(() => { monitorService.StartMonitor(this.Addresses); });
                    Monitor.Start();

                    this.ViewModel.BtnStartBackgroundColor = new SolidColorBrush(Color.FromRgb(200, 25, 25));
                    this.ViewModel.BtnStartContent = "Stop!";
                }
            }

            public void OnCanExecuteChanged()
            {
                EventHandler handler = this.CanExecuteChanged;
                if (handler != null)
                {
                    handler.Invoke(this, EventArgs.Empty);
                }
            }
        }



    }
}
