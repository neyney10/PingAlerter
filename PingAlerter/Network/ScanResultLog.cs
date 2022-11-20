using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PingAlerter.Network
{
    public class ScanResultLog
    {
        public string Address { get; }
        public long Min { get; }
        public long Max { get; }
        public long Avg { get; }
        public int Failed { get; }

        public ScanResultLog(string address, long min, long max, long avg, int failed)
        {
            this.Address = address;
            this.Min = min;
            this.Max = max;
            this.Avg = avg;
            this.Failed = failed;
        }

        public JsonNode ToJson()
        {
            return new JsonObject
            {
                ["Address"] = this.Address,
                ["Min"] = this.Min,
                ["Max"] = this.Max,
                ["Avg"] = this.Avg,
                ["Failed"] = this.Failed
            };

        }
    }
}
