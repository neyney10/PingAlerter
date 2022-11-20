using PingAlerter.Network;
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
        ObservableCollection<LogItem> logs;

        public ObservableCollection<LogItem> Logs
        {
            get { return logs; }
        }

        public LogViewModel()
        {
            logs = new ObservableCollection<LogItem>();
        }

        public LogViewModel(ObservableCollection<LogItem> logCollection)
        {
            logs = logCollection;
        }


        public void AddLog(Scan scan)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate
            {
                foreach(var entry in scan.Results)
                {
                    string address = entry.Key;
                    long latency = entry.Value.Avg;
                    this.logs.Add(new LogItem(scan.Timestamp, address, latency));
                }
            });
        }

        public class LogItem
        {
            public DateTime Timestamp { get; }
            public string Address { get; }
            public string Latency { get; }

            public LogItem(DateTime timestamp, string address, long latency)
            {
                this.Timestamp = timestamp;
                this.Address = address;
                this.Latency = latency.ToString() + "ms";
            }
        }
    }
}
