using PingAlerter.IO;
using PingAlerter.IO.FileSystem;
using PingAlerter.Other.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.Models
{
    public class LogConfigModel
    {
        #region Private data members
        private FileLogger Logger;
        #endregion

        #region Properties
        public string Filepath
        {
            get { return this.Logger.Filepath; }
            set { this.Logger.Filepath = value; }
        }

        #endregion

        #region Constructor
        public LogConfigModel(String logFilepath)
        {
            this.Logger = new FileLogger(logFilepath);
        }
        #endregion

        #region Methods
        public void SaveLog(LogData log)
        {
            this.Logger.SaveLog(log);
        }
        #endregion
    }
}
