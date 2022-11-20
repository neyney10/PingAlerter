using PingAlerter.Network;
using PingAlerter.Other.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.IO
{
    public interface ILogger
    {
        void SaveLog(Scan scan);
        //void SaveLog(ScanLog scanLog);

        IReadOnlyCollection<ScanLog> ReadLogs();
    }
}
