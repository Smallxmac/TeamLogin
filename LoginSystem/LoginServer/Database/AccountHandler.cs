using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Enums;
using LoginServer.IO;
using LoginServer.Objects;

namespace LoginServer.Database
{
    public class AccountHandler
    {

        private readonly DatabaseHandler _dbHandler;
        private readonly string _regQuarry;
        private readonly string _loginQuarry;
        private readonly string _logEmailQuarry;
        private readonly string _logIdQuarry;
        private readonly string _updateQuarry;

        public string LastError;
        public DateTime BanDateTime;

        /// <summary>
        /// Creates a database connection. And creates all of the SQL statements.
        /// </summary>
        public AccountHandler()
        {
            
            string table = Properties.Settings.Default.AccountTable;
            _dbHandler = new DatabaseHandler(Properties.Settings.Default.ConnectionString);
            _regQuarry =
                "INSERT INTO " + table + "(AccountUsername, AccountPassword, AccountEmail, AccountActivated, AccountBanned, AccountBanExpire, AccountRegisterDate, AccountPermission, AccountActivationCode) VALUES (@username, @password, @email, @activated, @banned, @banexpire,  @regdate, @permission, @code)";
            _loginQuarry =
                "SELECT * FROM " + table + " WHERE AccountUsername = @username";
            _logEmailQuarry =
                "SELECT * FROM " + table + " WHERE AccountEmail = @email";
            _logIdQuarry =
                "SELECT * FROM " + table + " WHERE UID = @id";
            _updateQuarry = "UPDATE " + table +
                            " SET AccountPassword = @password, AccountEmail = @email, AccountActivated = @activated, AccountBanned = @banned, AccountBanExpire = @banexpire, AccountPermission = @permission WHERE AccountUsername = @username";

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
            var account = GetAccount(username);

            if (account.AccountEmail != null)
            {
                if (account.Error)
                    return AccountStatus.ServerError;

                if (checkingName)
                    return AccountStatus.AccountNameUsed;

                if (password.Equals(account.AccountPassword))
                {
                    if (account.AccountBanned)
                    {
                        BanDateTime = account.AccountBanExpire;
                        return AccountStatus.AccountBanned;
                    }
                    if (!account.AccountActivated)
                        return AccountStatus.AccountNotActivated;

                    return AccountStatus.AccountAuthenicated;
                }
                if (password.Equals(account.AccountActivationCode) && !account.AccountActivated)
                {
                    account.AccountActivated = true;
                    if (SaveAccount(account))
                        return AccountStatus.AccountActivated;

                    return AccountStatus.ServerError;
                }
                return AccountStatus.AccountInvalid;
            }
            return AccountStatus.AccountInvalid;
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
            var account = GetAccountEmail(email);

            if (account.AccountUsername != null)
            {
                if (account.Error)
                    return AccountStatus.ServerError;

                if (checkingName)
                    return AccountStatus.AccountEmailUsed;

                if (password.Equals(account.AccountPassword))
                {
                    if (account.AccountBanned)
                    {
                        BanDateTime = account.AccountBanExpire;
                        return AccountStatus.AccountBanned;
                    }
                    if (!account.AccountActivated)
                        return AccountStatus.AccountNotActivated;

                    return AccountStatus.AccountAuthenicated;
                }
                if (password.Equals(account.AccountActivationCode) && !account.AccountActivated)
                {
                    account.AccountActivated = true;
                    if (SaveAccount(account))
                        return AccountStatus.AccountActivated;

                    return AccountStatus.ServerError;
                }
                return AccountStatus.AccountInvalid;
            }
            return AccountStatus.AccountInvalid;
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
            if (CheckAccount(username, password, true) != AccountStatus.AccountNameUsed)
            {
                if (CheckAccountEmail(email, password, true) != AccountStatus.AccountEmailUsed)
                {

                    var account = new Account();
                    account.AccountUsername = username;
                    account.AccountPassword = password;
                    account.AccountEmail = email;
                    account.AccountActivated = false;
                    account.AccountBanned = false;
                    account.AccountRegisterDate = DateTime.Now;
                    account.AccountPermission = AccountPermission.Normal;
                    account.AccountActivationCode = Guid.NewGuid().ToString();

                    if (SaveAccount(account))
                    {
                        EmailSender.SendVerification(account);
                        return AccountStatus.AccountCreated;
                    }
                    return AccountStatus.ServerError;
                }
                return AccountStatus.AccountEmailUsed;
            }
            return AccountStatus.AccountNameUsed;
        }

        /// <summary>
        /// Gathers the account from the database based on the username.
        /// </summary>
        /// <param name="username">The username of the account that will be grabbed.</param>
        /// <returns>Returns the account in an account object.</returns>
        public Account GetAccount(string username)
        {
            var account = new Account();

            try
            {
                _dbHandler.Conn.Open();
                _dbHandler.Cmd.Parameters.Clear();
                _dbHandler.Cmd.CommandText = _loginQuarry;
                _dbHandler.Cmd.Prepare();

                _dbHandler.Cmd.Parameters.AddWithValue("@username", username);
                MySql.Data.MySqlClient.MySqlDataReader reader = _dbHandler.Cmd.ExecuteReader();

                if (reader.Read())
                {
                    account.UID = reader.GetInt32("UID");
                    account.AccountUsername = username;
                    account.AccountPassword = reader.GetString("AccountPassword");
                    account.AccountEmail = reader.GetString("AccountEmail");
                    account.AccountActivated = reader.GetBoolean("AccountActivated");
                    account.AccountBanned = reader.GetBoolean("AccountBanned");
                    if (account.AccountBanned)
                        account.AccountBanExpire = reader.GetDateTime("AccountBanExpire");
                    account.AccountRegisterDate = reader.GetDateTime("AccountRegisterDate");
                    account.AccountPermission = (AccountPermission)Enum.Parse(typeof(AccountPermission), reader.GetString("AccountPermission"));
                    account.AccountActivationCode = reader.GetString("AccountActivationCode");
                }
            }

            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                if (_dbHandler.Conn != null)
                    _dbHandler.Conn.Close();
                LastError = exception.Message;
                account.Error = true;
            }

            finally
            {
                if (_dbHandler.Conn != null)
                    _dbHandler.Conn.Close();
            }

            return account;
        }

        /// <summary>
        /// Saves an account object in the database
        /// If the account exist it will update it.
        /// </summary>
        /// <param name="account">The account that will be saved.</param>
        /// <returns>Did it work?</returns>
        public bool SaveAccount(Account account)
        {
            try
            {
                if (CheckAccount(account.AccountUsername, account.AccountPassword, true) ==
                    AccountStatus.AccountNameUsed)
                {
                    //" SET AccountPassword = @password, AccountEmail = @email, AccountActivated = @activated, AccountBanned = @banned, AccountBanExpire = @banexpire, AccountPermission = @permission WHERE AccountUsername = @username";
                    _dbHandler.Conn.Open();
                    _dbHandler.Cmd.Parameters.Clear();
                    _dbHandler.Cmd.CommandText = _updateQuarry;
                    _dbHandler.Cmd.Parameters.AddWithValue("@password", account.AccountPassword);
                    _dbHandler.Cmd.Parameters.AddWithValue("@email", account.AccountEmail);
                    _dbHandler.Cmd.Parameters.AddWithValue("@activated", account.AccountActivated);
                    _dbHandler.Cmd.Parameters.AddWithValue("@banned", account.AccountBanned);
                    _dbHandler.Cmd.Parameters.AddWithValue("@banexpire", account.AccountBanExpire);
                    _dbHandler.Cmd.Parameters.AddWithValue("@permission", account.AccountPermission.ToString());
                    _dbHandler.Cmd.Parameters.AddWithValue("@username", account.AccountUsername);

                    _dbHandler.Cmd.ExecuteNonQuery();
                    return true;
                }
                _dbHandler.Conn.Open();
                _dbHandler.Cmd.Parameters.Clear();
                _dbHandler.Cmd.CommandText = _regQuarry;
                _dbHandler.Cmd.Prepare();

                _dbHandler.Cmd.Parameters.AddWithValue("@username", account.AccountUsername);
                _dbHandler.Cmd.Parameters.AddWithValue("@password", account.AccountPassword);
                _dbHandler.Cmd.Parameters.AddWithValue("@email", account.AccountEmail);
                _dbHandler.Cmd.Parameters.AddWithValue("@activated", account.AccountActivated);
                _dbHandler.Cmd.Parameters.AddWithValue("@banned", account.AccountBanned);
                _dbHandler.Cmd.Parameters.AddWithValue("@banexpire", account.AccountBanExpire);
                _dbHandler.Cmd.Parameters.AddWithValue("@regdate", account.AccountRegisterDate);
                _dbHandler.Cmd.Parameters.AddWithValue("@permission", account.AccountPermission.ToString());
                _dbHandler.Cmd.Parameters.AddWithValue("@code", account.AccountActivationCode);

                _dbHandler.Cmd.ExecuteNonQuery();
                return true;
            }

            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                if (_dbHandler.Conn != null)
                    _dbHandler.Conn.Close();
                LastError = exception.Message;
                return false;
            }

            finally
            {
                if (_dbHandler.Conn != null)
                    _dbHandler.Conn.Close();
            }
        }

        /// <summary>
        /// Gathers and account out of the database based on account Id
        /// </summary>
        /// <param name="email">The account email that will be pulled.</param>
        /// <returns>Returns the account in an account object.</returns>
        public Account GetAccountId(int id)
        {
            var account = new Account();

            try
            {
                _dbHandler.Conn.Open();
                _dbHandler.Cmd.Parameters.Clear();
                _dbHandler.Cmd.CommandText = _logIdQuarry;
                _dbHandler.Cmd.Prepare();

                _dbHandler.Cmd.Parameters.AddWithValue("@id", id);
                MySql.Data.MySqlClient.MySqlDataReader reader = _dbHandler.Cmd.ExecuteReader();

                if (reader.Read())
                {
                    account.UID = id;
                    account.AccountUsername = reader.GetString("AccountUsername");
                    account.AccountPassword = reader.GetString("AccountPassword");
                    account.AccountEmail = reader.GetString("AccountEmail");
                    account.AccountActivated = reader.GetBoolean("AccountActivated");
                    account.AccountBanned = reader.GetBoolean("AccountBanned");
                    if (account.AccountBanned)
                        account.AccountBanExpire = reader.GetDateTime("AccountBanExpire");
                    account.AccountRegisterDate = reader.GetDateTime("AccountRegisterDate");
                    account.AccountPermission = (AccountPermission)Enum.Parse(typeof(AccountPermission), reader.GetString("AccountPermission"));
                    account.AccountActivationCode = reader.GetString("AccountActivationCode");
                }
            }

            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                if (_dbHandler.Conn != null)
                    _dbHandler.Conn.Close();
                LastError = exception.Message;
                account.Error = true;
            }

            finally
            {
                if (_dbHandler.Conn != null)
                    _dbHandler.Conn.Close();
            }

            return account;
        }

        /// <summary>
        /// Gathers and account out of the database based on email.
        /// </summary>
        /// <param name="email">The account email that will be pulled.</param>
        /// <returns>Returns the account in an account object.</returns>
        public Account GetAccountEmail(string email)
        {
            var account = new Account();

            try
            {
                _dbHandler.Conn.Open();
                _dbHandler.Cmd.Parameters.Clear();
                _dbHandler.Cmd.CommandText = _logEmailQuarry;
                _dbHandler.Cmd.Prepare();

                _dbHandler.Cmd.Parameters.AddWithValue("@email", email);
                MySql.Data.MySqlClient.MySqlDataReader reader = _dbHandler.Cmd.ExecuteReader();

                if (reader.Read())
                {
                    account.UID = reader.GetInt32("UID");
                    account.AccountUsername = reader.GetString("AccountUsername");
                    account.AccountPassword = reader.GetString("AccountPassword");
                    account.AccountEmail = email;
                    account.AccountActivated = reader.GetBoolean("AccountActivated");
                    account.AccountBanned = reader.GetBoolean("AccountBanned");
                    if (account.AccountBanned)
                        account.AccountBanExpire = reader.GetDateTime("AccountBanExpire");
                    account.AccountRegisterDate = reader.GetDateTime("AccountRegisterDate");
                    account.AccountPermission = (AccountPermission)Enum.Parse(typeof(AccountPermission), reader.GetString("AccountPermission"));
                    account.AccountActivationCode = reader.GetString("AccountActivationCode");
                }
            }

            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                if (_dbHandler.Conn != null)
                    _dbHandler.Conn.Close();
                LastError = exception.Message;
                account.Error = true;
            }

            finally
            {
                if (_dbHandler.Conn != null)
                    _dbHandler.Conn.Close();
            }

            return account;
        }
    }
}
