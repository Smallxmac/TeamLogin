using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginSystem.Enums;

namespace LoginSystem.Handlers
{
    public static class AccountHandler
    {

        /// <summary>
        /// Check the username and password with records in the account database.
        /// If checking accounts is set it will return if the username exist or not.
        /// </summary>
        /// <param name="username">Username in which the quary will look for.</param>
        /// <param name="password">Password that will be compaired once the record has been gathered.</param>
        /// <param name="checkingName">Boolean to tell if we are just checking if the name exist or not.</param>
        /// <returns>Returns the status of the account gathered. Statuses are in Enums.AccountStatus</returns>
        public static AccountStatus CheckAccount(string username, string password, bool checkingName)
        {
            //TODO: Add logic code
            return AccountStatus.Error;
        }

        /// <summary>
        /// Will try to add the account into the account database with the information
        /// that has been given.
        /// </summary>
        /// <param name="username">The username of the account that will be registed.</param>
        /// <param name="password">The password of the account that will be registed.</param>
        /// <param name="email">The E-Mail of the account that will be registed.</param>
        /// <returns>Returns the status of registration EX: AccountCreated, Error, or AccountNameUsed</returns>
        public static AccountStatus RegisterAccount(string username, string password, string email)
        {
            //TODO: Add logic code
            return AccountStatus.Error;
        }
    }
}
