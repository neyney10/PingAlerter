using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using PingAlerter.Network;
using PingAlerter.Other.Log;

namespace PingAlerter.IO.FileSystem
{
    class FileLogger : ILogger
    {

        public string Filepath { get; set; }
        

        public FileLogger(string filepath)
        {
            this.Filepath = filepath;
        }

        public IEnumerable<string> Read()
        {
            List<string> lines = new List<string>();

            using (StreamReader inputFile = new StreamReader(this.Filepath, true))
            {
                string line = inputFile.ReadLine();
                lines.Add(line);
            }

            return lines;
        }

        public void Write(string text)
        {
            throw new NotImplementedException();
        }

        public void WriteSingle(Object stringable)
        {
            WriteSingle(stringable.ToString());
        }

        public void WriteSingle(string text)
        {
            // Append text.
            using (StreamWriter outputFile = new StreamWriter(this.Filepath, true))
            {
                outputFile.WriteLine(text);
            }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void SaveLog(Scan scan)
        {
            WriteSingle(scan.ToLog().ToJson().ToJsonString());
        }

        public IReadOnlyCollection<ScanLog> ReadLogs()
        {
            List<ScanLog> logs = new List<ScanLog>();

            IEnumerable<string> lines = Read();

            // convert from lines of string to lines of Logs
            foreach(string line in lines)
            {
                logs.Add(
                    ScanLog.FromJson(
                        JsonNode.Parse(line)
                    )
                );
            }

            return logs;
        }
    }
}
