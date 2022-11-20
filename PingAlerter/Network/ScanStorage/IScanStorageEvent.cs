using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Network.ScanStorage
{
    public enum Type { add, remove };
    public interface IScanStorageEvent
    {
        Type Type { get; }
        ScanStorage Storage { get; }
    }
}
