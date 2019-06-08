using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.IO.Database
{
    public interface IDBConnection
    {
        void Connect();
        void Disconnect();
        bool IsConnected();
        IDataReader Retrieve(string query); //Query
        bool Store(string query); //Query.
    }
}
