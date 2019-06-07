using PingAlerter.Other.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Other.MonitorTab
{
    public class MonitorServiceNotify
    {
        #region public readonly Properties
        /// <summary>
        /// the data itself of type LogData.
        /// </summary>
        public readonly LogData Data;

        /// <summary>
        /// Type of event, can notify about multiple things.
        /// </summary>
        public readonly Type eventType;

        /// <summary>
        /// Amount of scans so far.
        /// </summary>
        public readonly int ScanCount;
        #endregion

        #region Type ENUM
        /// <summary>
        /// Type of event to notify.
        /// </summary>
        public enum Type { Log, OverThreshold }
        #endregion

        #region Constructor
        public MonitorServiceNotify(LogData data, int scanCount, Type type)
        {
            this.Data = data;
            this.eventType = type;
            this.ScanCount = scanCount;
        }
        #endregion
    }
}
