using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Network.ScanStorage
{
    class ScanStorageAddEvent : IScanStorageEvent
    {
        public Type Type { get { return Type.add; } }
        public ICollection<Scan> AdditionalItems { get; }
        public ScanStorage Storage { get; }

        public ScanStorageAddEvent(ScanStorage storage, ICollection<Scan> additionalItems)
        {
            this.Storage = storage;
            this.AdditionalItems = additionalItems;
        }
    }
}
