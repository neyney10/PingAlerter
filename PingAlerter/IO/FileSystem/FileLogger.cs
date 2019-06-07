using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.IO.FileSystem
{
    class FileLogger
    {

        public string Filepath { get; set; }
        

        public FileLogger(string filepath)
        {
            this.Filepath = filepath;
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

        }
    }
}
