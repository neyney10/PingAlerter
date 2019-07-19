using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;

namespace PingAlerter.IO.FileSystem
{
    /// <summary>
    /// Parses a text file that contains addresses/hosts into a List/object.
    /// </summary>
    public  static class AddressParser
    {
        /// <summary>
        /// Parse a file that is pointed by the file_path argument into list of addresses.
        /// </summary>
        /// <param name="file_path">Path to the text file.</param>
        /// <returns></returns>
        public static IEnumerable<string> Parse(string file_path)
        {
            // setup statistics.
            int invalid_lines = 0;
            int total_lines = 0;

            // open file for reading.
            FileStream fs = new FileStream(file_path, FileMode.Open, FileAccess.Read);
 
            // create a list to store all addresses.
            List<string> address_list = new List<string>();

            // setup address format validation tools.
            Regex rx = new Regex(@"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}");

            // iterate over lines in the text file.
            // using streamReader with encoding of UTF8 to read text as english characters.
            using (var streamReader = new StreamReader(fs, Encoding.UTF8))
            {
                string line;

                while (fs.CanRead && (line = streamReader.ReadLine()) != null)
                {
                    ++total_lines;

                    // validation.
                    if (line.Length < 20 && rx.IsMatch(line))
                    { // if valid then add to list.
                        // add line to address list.
                        address_list.Add(line);
                    }
                    else //if invalid.
                        ++invalid_lines; 
                }
            }

            return address_list;
        }
        
    }
}
