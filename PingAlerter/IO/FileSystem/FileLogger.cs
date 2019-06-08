using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void SaveLog(LogData log)
        {
            WriteSingle(log);
        }

        public IEnumerable<LogData> ReadLogs()
        {
            List<LogData> logs = new List<LogData>();

            IEnumerable<string> lines = Read();

            // convert from lines of string to lines of LogData
            foreach(string line in lines)
            {
                int timestampEnd = line.LastIndexOf(":");
                int tagEnd = line.IndexOf("]");
                int addressEnd = line.IndexOf(",");

                // TODO: create a class/function which parses it.
                LogData log = new LogData(
                    DateTime.Parse(line.Substring(0, timestampEnd)),
                    line.Substring(timestampEnd+1,tagEnd - timestampEnd ),
                    line.Substring(tagEnd+1,addressEnd-tagEnd-1),
                    line.Substring(addressEnd+1,line.Length-addressEnd-1));

                logs.Add(log);
            }

            return logs;
        }
    }
}
