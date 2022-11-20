using PingAlerter.Network;
using PingAlerter.Network.ScanStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Common.State
{
    class State
    {
        public static ScanStorage Scans = new ScanStorage();
        public static bool IsOverlayEnabled = false;
    }
}
