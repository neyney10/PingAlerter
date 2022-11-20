using PingAlerter.Network;
using PingAlerter.Other.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PingAlerter.Network.ScanAlert;

namespace PingAlerter.Other.MonitorTab
{
    public class MonitorServiceNotify
    {
        #region public readonly Properties
        public Scan Scan { get; }

        #endregion

        #region Type ENUM
        /// <summary>
        /// Type of event to notify.
        /// </summary>
        public enum Type { Log, OverThreshold }
        #endregion

        #region Constructor
        public MonitorServiceNotify(Scan scan)
        {
            this.Scan = scan;
        }
        #endregion
    }
}
