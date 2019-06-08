using MySql.Data.MySqlClient;
using PingAlerter.Other.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingAlerter.IO.Database 
{
    /// <summary>
    ///  https://stackoverflow.com/questions/21618015/how-to-connect-to-mysql-database.
    /// Wrapper class for MySqlClient
    /// </summary>
    public class DBMySQL: IDBConnection
    {
        #region Properties
        private MySqlConnection Connection;
        #endregion

        #region Constructor
        public DBMySQL(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }
        #endregion

        #region IDBConnection Methods

        public void Connect()
        {
            Connection.Open();
        }

        public void Disconnect()
        {
            if(IsConnected())
                Connection.Close();
        }

        public bool IsConnected()
        {
            ConnectionState state = Connection.State;
            return (state == ConnectionState.Open || state == ConnectionState.Executing || state == ConnectionState.Fetching);
        }

        public IDataReader Retrieve(string query)
        {
            if (!IsConnected())
                throw new Exception("Cannot Retrieve data from MySQL Database as the connection isn't connected.");

            // create Query command.
            var cmd = new MySqlCommand(query, Connection);

            // Execute Query.
            var reader = cmd.ExecuteReader();

            return reader;
        }

        public bool Store(string query)
        {
            if (!IsConnected())
                throw new Exception("Cannot Retrieve data from MySQL Database as the connection isn't connected.");

            bool success;

            // create Query command.
            int amount_of_rows_affected = new MySqlCommand(query, Connection).ExecuteNonQuery();

            if (amount_of_rows_affected > 0)
                success = true;
            else success = false;

            return success;
        }

        #endregion

    }
}
