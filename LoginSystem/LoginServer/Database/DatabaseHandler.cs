using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LoginServer.Database
{
    public class DatabaseHandler
    {
        public MySqlConnection Conn;
        public MySqlCommand Cmd;
        /// <summary>
        /// This is the constructor for the Database connection and handler.
        /// Builds the connection and command link.
        /// </summary>
        /// <param name="connectionString">The connection string to the server.</param>
        public DatabaseHandler(string connectionString)
        {
            Conn = new MySqlConnection(connectionString);
            Cmd = new MySqlCommand();
            Cmd.Connection = Conn;
        }
    }
}
