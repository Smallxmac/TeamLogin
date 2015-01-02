using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LoginSystem.Enums;
using LoginSystem.IO.Database;

namespace LoginSystem.Handlers
{
    public class AccountHandler
    {

        private readonly DatabaseHandler _dbHandler;
        private readonly string _regQuarry;
        private readonly string _loginQuarry;
        private readonly string _logEmailQuarry;
        public string lastError;
        public DateTime BanDateTime;

        /// <summary>
        /// Creates a database connection.
        /// </summary>
        public AccountHandler()
        {
            string table = Properties.Settings.Default.AccountTable;
            _dbHandler = new DatabaseHandler(Properties.Settings.Default.ConnectionString);
            _regQuarry = "INSERT INTO " + table + "(AccountUsername, AccountPassword, AccountEMail, AccountRegisterDate, AccountPremission) VALUES (@username, @password, @email, @date, @permission)";
            _loginQuarry =
                "SELECT AccountPassword, AccountActivated, AccountBanned, AccountBanExpire FROM "+ table +" WHERE AccountUsername = @username";
            _logEmailQuarry =
                "SELECT AccountPassword, AccountActivated, AccountBanned, AccountBanExpire FROM "+ table +" WHERE AccountEmail = @email";
        }

        /// <summary>
        /// Check the username and password with records in the account database.
        /// If checking accounts is set it will return if the username exist or not.
        /// </summary>
        /// <param name="username">Username in which the quarry will look for.</param>
        /// <param name="password">Password that will be compared once the record has been gathered.</param>
        /// <param name="checkingName">Boolean to tell if we are just checking if the name exist or not.</param>
        /// <returns>Returns the status of the account gathered. Statuses are in Enums.AccountStatus</returns>
        public AccountStatus CheckAccount(string username, string password, bool checkingName)
        {
        
            try
            {
                _dbHandler.Conn.Open();
                _dbHandler.Cmd.CommandText = _loginQuarry;
                _dbHandler.Cmd.Prepare();

                _dbHandler.Cmd.Parameters.AddWithValue("@username", username);
                MySql.Data.MySqlClient.MySqlDataReader reader = _dbHandler.Cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (checkingName)
                        return AccountStatus.AccountNameUsed;
                    if (password.Equals(reader.GetString("AccountPassword")))
                    {
                        if (reader.GetBoolean("AccountBanned"))
                        {
                            BanDateTime = reader.GetDateTime("AccountBanExpire");
                            return AccountStatus.AccountBanned;
                        }
                        else if (!reader.GetBoolean("AccountActivated"))
                            return AccountStatus.AccountNotActivated;
                        else
                            return AccountStatus.AccountAuthenicated;
                    }
                    else
                        return AccountStatus.AccountInvalid;
                }
                else
                    return AccountStatus.AccountInvalid;

            }
            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                if(_dbHandler != null)
                _dbHandler.Conn.Close();
                lastError = exception.Message;
                return AccountStatus.ServerError;
            }
            finally
            {
                if (_dbHandler != null)
                    _dbHandler.Conn.Close();
            }
            _dbHandler.Conn.Open();
            return AccountStatus.AccountInvalid;
            //TODO: Add logic code
        }

        /// <summary>
        /// Check the username and password with records in the account database.
        /// If checking accounts is set it will return if the username exist or not.
        /// </summary>
        /// <param name="email">The Email that will be used to grab the account.</param>
        /// <param name="password">Password that will be compared once the record has been gathered.</param>
        /// <param name="checkingName">Boolean to tell if we are just checking if the name exist or not.</param>
        /// <returns>Returns the status of the account gathered. Statuses are in Enums.AccountStatus</returns>
        public AccountStatus CheckAccountEmail(string email, string password, bool checkingName)
        {
            try
            {
                _dbHandler.Conn.Open();
                _dbHandler.Cmd.CommandText = _logEmailQuarry;
                _dbHandler.Cmd.Prepare();

                _dbHandler.Cmd.Parameters.AddWithValue("@logEmail", email);
                MySql.Data.MySqlClient.MySqlDataReader reader = _dbHandler.Cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (checkingName)
                        return AccountStatus.AccountEmailUsed;

                    if (password.Equals(reader.GetString("AccountPassword")))
                    {
                        if (reader.GetBoolean("AccountBanned"))
                        {
                            BanDateTime = reader.GetDateTime("AccountBanExpire");
                            return AccountStatus.AccountBanned;
                        }
                        else if (!reader.GetBoolean("AccountActivated"))
                            return AccountStatus.AccountNotActivated;
                        else
                            return AccountStatus.AccountAuthenicated;
                    }
                    else
                        return AccountStatus.AccountInvalid;
                }
                else
                    return AccountStatus.AccountInvalid;

            }

            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                if (_dbHandler != null)
                    _dbHandler.Conn.Close();
                lastError = exception.Message;
                return AccountStatus.ServerError;
            }

            finally
            {
                if (_dbHandler != null)
                    _dbHandler.Conn.Close();
            }
        }

        /// <summary>
        /// Will try to add the account into the account database with the information
        /// that has been given.
        /// </summary>
        /// <param name="username">The username of the account that will be registered.</param>
        /// <param name="password">The password of the account that will be registered.</param>
        /// <param name="email">The E-Mail of the account that will be registered.</param>
        /// <returns>Returns the status of registration EX: AccountCreated, Error, or AccountNameUsed</returns>
        public AccountStatus RegisterAccount(string username, string password, string email)
        {
            try
            {
                _dbHandler.Conn.Open();
                _dbHandler.Cmd.CommandText = _regQuarry;
                _dbHandler.Cmd.Prepare();

                _dbHandler.Cmd.Parameters.AddWithValue("@username", username);
                _dbHandler.Cmd.Parameters.AddWithValue("@password", password);
                _dbHandler.Cmd.Parameters.AddWithValue("@email", email);
                _dbHandler.Cmd.Parameters.AddWithValue("@date", DateTime.Now);
                _dbHandler.Cmd.Parameters.AddWithValue("@permission", AccountPermission.Normal.ToString());

                _dbHandler.Cmd.ExecuteNonQuery();
                return AccountStatus.AccountCreated;
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                if (_dbHandler.Conn != null)
                    _dbHandler.Conn.Close();
                lastError = e.Message;
                return AccountStatus.ServerError;
            }
            finally
            {
                if (_dbHandler.Conn != null)
                    _dbHandler.Conn.Close();
            }
            //TODO: Add logic code
            return AccountStatus.Error;
        }
    }
}
