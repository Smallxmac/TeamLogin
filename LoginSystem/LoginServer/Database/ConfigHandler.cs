using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Database
{
    public class ConfigHandler
    {
        public Dictionary<string, string> ConfigDictionary;
        public string LastError;
        private readonly DatabaseHandler _dbHandler;

        public ConfigHandler(string connectionString)
        {
            _dbHandler = new DatabaseHandler(connectionString);
        }
        public bool LoadConfig()
        {
            try
            {
                _dbHandler.Conn.Open();
                _dbHandler.Cmd.CommandText = "SELECT * FROM " + Properties.Settings.Default.ConfigTable;
                MySql.Data.MySqlClient.MySqlDataReader reader = _dbHandler.Cmd.ExecuteReader();
                ConfigDictionary = new Dictionary<string, string>();
                while (reader.Read())
                {
                    ConfigDictionary.Add(reader.GetString("Key"), reader.GetString("Value"));
                }
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                if (_dbHandler != null)
                    _dbHandler.Conn.Close();
                LastError = exception.Message;
                return false;
            }
            finally
            {
                if (_dbHandler != null)
                    _dbHandler.Conn.Close();
            }
        }
    }
}
