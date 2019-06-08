using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingAlerter.Other.Log;

namespace PingAlerter.IO.Database
{
    class DBMySQLLogger : DBMySQL, ILogger
    {
        public DBMySQLLogger(String connectionString) : base(connectionString)
        { }

        public IEnumerable<LogData> ReadLogs()
        {
            if (!IsConnected())
                this.Connect();

            var resultsReader = this.Retrieve("SELECT * FROM logs;");

            List<LogData> logs = new List<LogData>();

            while(resultsReader.Read())
            {
                LogData log = new LogData(
                    DateTime.Parse((string) resultsReader["Timestamp"]),
                    (string) resultsReader["Tag"],
                    (string) resultsReader["Address"],
                    (string) resultsReader["Value"]);

                logs.Add(log);
            }

            this.Disconnect();

            return logs;

        }

        public void SaveLog(LogData log)
        {
            if (!IsConnected())
                this.Connect();

            this.Store(string.Format("INSERT INTO logs (Timestamp, Tag, Address, Value,ID) VALUES ('{0}','{1}', '{2}', '{3}',default);",log.Timestamp, log.LogTag,log.Address,log.Value));

            this.Disconnect();
        }
    }
}
