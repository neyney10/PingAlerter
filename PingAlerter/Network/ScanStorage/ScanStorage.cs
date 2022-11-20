using PingAlerter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Network.ScanStorage
{
    public class ScanStorage : Observable<IScanStorageEvent>
    {
        private readonly List<Scan> scans;
        public IReadOnlyCollection<Scan> Scans { get { return this.scans; } }

        public ScanStorage()
        {
            this.scans = new List<Scan>();
        }

        public void Add(IEnumerable<Scan> scans)
        {
            foreach (var scan in scans)
            {
                this._add(scan);
            }

            this.NotifyObservers(
                  new ScanStorageAddEvent(this, scans.ToList())
            ) ;
        }

        public void Add(Scan scan)
        {
            this._add(scan);
            this.NotifyObservers(
                new ScanStorageAddEvent(this, new List<Scan> { scan })
            );
        }

        public void Clear()
        {
            this.scans.Clear();
        }

        private void _add(Scan scan)
        {
            this.scans.Add(scan);
        }
    }
}
