using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using PingAlerter.Other.Log;

namespace PingAlerter.Models
{ 

    public class LogModel
    {
        private List<LogData> logs;
        public List<LogData> Logs
        {
            get { return logs; }
            protected set { logs = value; }
        }

        public LogModel()
        {
            logs = new List<LogData>();
        }

        public void AddLog(LogData log)
        {
            Logs.Add(log);
        }

    }
}
