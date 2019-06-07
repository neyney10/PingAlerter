
using PingAlerter.IO.FileSystem;
using PingAlerter.Network;
using PingAlerter.Other.Log;
using PingAlerter.Other.MainWindow;
using PingAlerter.Other.MonitorConfig;
using System;
using System.Collections.Generic;
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

namespace PingAlerter.Other.MonitorTab
{
    class MonitorTabViewModel : IObservable<MonitorServiceNotify>
    {
        // Model Interface and stuff
        private MonitorTabModel Model;

        public IEnumerable<string> Addresses { get { return this.Model.Addresses; } }
        public string txtb_newAddress { get { return Model.txtb_newAddress; } set { this.Model.txtb_newAddress = value; } }

        // this ViewModel interaction logic
        public StartLatencyMonitorCommand startLatencyMonitorCommand { get; set; }
        public Brush BtnStartBackgroundColor { get; set; }
        public string BtnStartContent { get; set; }

        // Observable memebrs
        private ISet<IObserver<MonitorServiceNotify>> Observers;

        // Temp data members
        Thread Monitor;
        FileLogger filelogger;
        System.Media.SoundPlayer player;
        MonitorService monitorService;


        // constructor
        public MonitorTabViewModel(LogViewModel logViewModel)
        {
            this.Model = new MonitorTabModel();
            this.Observers = new HashSet<IObserver<MonitorServiceNotify>>();
            
            
            this.monitorService = new MonitorService();
            MonitorObserver o = new MonitorObserver((data) => { NotifyObservers(data); });
            this.monitorService.Subscribe(o);
            this.startLatencyMonitorCommand = new StartLatencyMonitorCommand(monitorService,this.Model.Addresses);
        }

        public class StartLatencyMonitorCommand : ICommand
        {

            public event EventHandler CanExecuteChanged;

            private bool can_execute { get; set; }
            private readonly MonitorService monitorService;
            private Thread Monitor;
            private IEnumerable<string> Addresses;

            public bool canExecute {
                get { return can_execute; }
                set { this.can_execute = value; OnCanExecuteChanged(); }
            }

            public StartLatencyMonitorCommand(MonitorService monitorService, IEnumerable<string> addresses)
            {
                this.monitorService = monitorService;
                this.Addresses = addresses;
                can_execute = true;
            }

            public bool CanExecute(object parameter)
            {
                return this.canExecute;
            }

            public void Execute(object parameter)
            {
                if (1==2)
                {
                    canExecute = false;
                    Monitor.Abort(); // TODO: change it to stop the thread using a boolean or somethin.

                    //BtnStartBackgroundColor = new LinearGradientBrush(Color.FromRgb(25, 200, 33), Color.FromRgb(0, 233, 88), 1);
                    //BtnStartContent = "Start!";

                }
                else
                {
                    canExecute = true;

                    //LatencyMonitor latencyMonitor = new LatencyMonitor();
                    //latencyMonitor.PreCheck(Addresses, 5, 4, 30);
                    
                    Monitor = new Thread(() => { monitorService.StartMonitor(this.Addresses); });
                    Monitor.Start();

                    //BtnStartBackgroundColor = new SolidColorBrush(Color.FromRgb(200, 25, 25));
                    //BtnStartContent = "Stop!";
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

        class MonitorObserver : IObserver<MonitorServiceNotify>
        {
            private Action<MonitorServiceNotify> Callback;

            public MonitorObserver(Action<MonitorServiceNotify> callback)
            {
                this.Callback = callback;
            }

            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(MonitorServiceNotify value)
            {
                this.Callback(value); 
            }
        }


        #region Observable methods and helper classes

        public void NotifyObservers(MonitorServiceNotify newData)
        {
            foreach (IObserver<MonitorServiceNotify> observer in this.Observers)
                observer.OnNext(newData);
        }

        public IDisposable Subscribe(IObserver<MonitorServiceNotify> observer)
        {
            this.Observers.Add(observer);
            return new Unsubscriber(this.Observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private ISet<IObserver<MonitorServiceNotify>> _observers;
            private IObserver<MonitorServiceNotify> _observer;

            public Unsubscriber(ISet<IObserver<MonitorServiceNotify>> observers, IObserver<MonitorServiceNotify> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);

            }
        }

        #endregion
    }
}
