using PingAlerter.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Network
{
    class ScanHistory
    {
        public CircularBuffer<ScanResult> Results { get; protected set; }
        private long Sum;
        public long Avg { get { return (Results.Count > 0) ? Sum / Results.Count : -1; } }

        public ScanHistory(int Capacity)
        { this.Results = new CircularBuffer<ScanResult>(Capacity); }

        public void AddResult(ScanResult sr)
        {
            ScanResult removed_result = this.Results.Enqueue(sr);
            if (removed_result != null) // if removed
                this.Sum -= removed_result.Avg;
            this.Sum += sr.Avg;
        }
    }
}
