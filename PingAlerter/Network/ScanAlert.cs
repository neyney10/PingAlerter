using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Network
{
    public class ScanAlert
    {
        public enum AlertType { 
            Spike,
        }

        public AlertType Type { get; }

        public ScanAlert(AlertType type)
        {
            this.Type = type;
        }
    }
}
