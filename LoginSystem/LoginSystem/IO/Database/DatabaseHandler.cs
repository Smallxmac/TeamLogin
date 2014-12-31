using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LoginSystem.IO.Database
{
    public class DatabaseHandler
    {
        public MySqlConnection Conn;
        public MySqlCommand Cmd;
        public DatabaseHandler(string connectionString)
        {
            Conn = new MySqlConnection(connectionString);
            Cmd = new MySqlCommand();
            Cmd.Connection = Conn;
        }
    }
}
