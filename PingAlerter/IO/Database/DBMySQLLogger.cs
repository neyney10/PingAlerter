using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingAlerter.Network;
using PingAlerter.Other.Log;

namespace PingAlerter.IO.Database
{
    class DBMySQLLogger : DBMySQL, ILogger
    {
        public DBMySQLLogger(String connectionString) : base(connectionString)
        { }

        public void SaveLog(Scan scan)
        {
            if (this.ConnectionString == "")
                return;

            if (!IsConnected())
                this.Connect();

            ScanLog log = scan.ToLog();

            ICollection<string> commands = new List<string>();
            // Scan
            commands.Add(
                string.Format(
                    "INSERT INTO scans (timestamp, id) VALUES ('{0}', default);",
                    this.SQLDateTimeFormat(log.Timestamp)
                )
            );
            commands.Add("SET @last_generated_scan_id = LAST_INSERT_ID();");

            // Results
            string cmd = "INSERT INTO results (address, min, max, avg, failed, id, scan_id) VALUES ";
            foreach (ScanResultLog resultLog in log.Results)
            {
                cmd += string.Format(
                    "('{0}','{1}', '{2}', '{3}', '{4}', default, @last_generated_scan_id)" + ",",
                    resultLog.Address, resultLog.Min, resultLog.Max, resultLog.Avg, resultLog.Failed
                );
            }
            cmd = cmd.Substring(0, cmd.Length - 1) + ';';
            commands.Add(cmd);

            // Alerts
            if (log.Alerts.Count > 0)
            {
                cmd = "INSERT INTO alerts (type, id, scan_id) VALUES ";
                foreach (ScanAlert alert in log.Alerts)
                {
                    cmd += string.Format(
                        "('{0}', default, @last_generated_scan_id)" + ",",
                        alert.Type.ToString()
                    );
                }
                cmd = cmd.Substring(0, cmd.Length - 1) + ';';
                commands.Add(cmd);
            }

            this.StoreTransaction(commands);
           

            //this.Disconnect();
        }

        public IReadOnlyCollection<ScanLog> ReadLogs()
        {
            if (this.ConnectionString == "")
                return new List<ScanLog>();

            if (!IsConnected())
                this.Connect();

            var resultsReader = this.Retrieve(
                "SELECT * " +
                "FROM scans " +
                "LEFT JOIN results " +
                "ON scans.id = results.scan_id;"
            );

            List<ScanLog> logs = new List<ScanLog>();
            List<ScanResultLog> results = null;
            DateTime timestamp = DateTime.Now;
            uint previous_scan_id = 0;
            bool isFirstRow = true;

            while(resultsReader.Read())
            {
                uint scan_id = (uint)resultsReader.GetValue(0);
                if (previous_scan_id != scan_id)
                {
                    if (!isFirstRow)
                    {
                        logs.Add(
                            new ScanLog(
                                timestamp,
                                results,
                                null
                            )
                        );
                    }
                    else isFirstRow = false;

                    previous_scan_id = scan_id;
                    results = new List<ScanResultLog>();
                    timestamp = (DateTime)resultsReader.GetValue(1);
                }

                results.Add(
                    new ScanResultLog(
                        (string)resultsReader.GetValue(8),
                        (uint)resultsReader.GetValue(3),
                        (uint)resultsReader.GetValue(4),
                        (uint)resultsReader.GetValue(5),
                        Convert.ToInt32(resultsReader.GetValue(6))
                    )
                );
            }

            resultsReader.Close();
            this.Disconnect();

            return logs;

        }

        private string SQLDateTimeFormat(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }

}
