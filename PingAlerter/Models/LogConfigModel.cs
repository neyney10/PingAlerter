using PingAlerter.IO;
using PingAlerter.IO.FileSystem;
using PingAlerter.IO.Database;
using PingAlerter.Other.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingAlerter.Network;

namespace PingAlerter.Models
{
    public class LogConfigModel
    {
        #region Private data members
        private FileLogger Logger;
        private DBMySQLLogger SQLLogger;
        #endregion

        #region Properties
        public string Filepath
        {
            get { return this.Logger.Filepath; }
            set { this.Logger.Filepath = value; }
        }
        public string MySQLConnString
        {
            get { return this.SQLLogger.ConnectionString; }
            set { this.SQLLogger = new DBMySQLLogger(value); }
        }

        #endregion

        #region Constructor
        public LogConfigModel(string logFilepath, string mySQLConnString)
        {
            this.Logger = new FileLogger(logFilepath);
            this.SQLLogger = new DBMySQLLogger(mySQLConnString);

            //var a = this.SQLLogger.ReadLogs();
        }
        #endregion

        #region Methods
        public void SaveLog(Scan scan)
        {
            this.Logger.SaveLog(scan);
            this.SQLLogger.SaveLog(scan);
        }
        #endregion
    }
}
