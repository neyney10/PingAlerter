using PingAlerter.Other.Log;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace PingAlerter.ViewModels
{
    /// <summary>
    /// ViewModel of LogData model class.
    /// </summary>
    public class LogViewModel
    {
        ObservableCollection<LogData> logs;

        public ObservableCollection<LogData> Logs
        {
            get { return logs; }
        }

        public LogViewModel()
        {
            logs = new ObservableCollection<LogData>();
        }

        public LogViewModel(ObservableCollection<LogData> logCollection)
        {
            logs = logCollection;
        }


        public void AddLog(LogData log)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                this.Logs.Add(log);
            });
        }



  
    }
}
