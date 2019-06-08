using PingAlerter.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Models
{
    public class SettingsModel
    {
        #region Properties
        public LatencyMonitorConfig monitorConfig { get; set; }
        public AlertConfigModel alertConfigModel { get; set; }
        public LogConfigModel logConfigModel { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constrcutor with default values.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="alertConfigModel"></param>
        public SettingsModel() : this(new LatencyMonitorConfig(), new AlertConfigModel(null), new LogConfigModel(null))
        { }

        public SettingsModel(LatencyMonitorConfig configuration, AlertConfigModel alertConfigModel, LogConfigModel logConfigModel)
        {
            this.monitorConfig = configuration;
            this.alertConfigModel = alertConfigModel;
            this.logConfigModel = logConfigModel;
        }
        #endregion
    }
}
