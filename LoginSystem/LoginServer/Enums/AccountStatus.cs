using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Enums
{
    /// <summary>
    /// AccountStatus is a set of enums which will be used in the login
    /// process to return information in a readable format.
    /// </summary>
    public enum AccountStatus : short
    {
        Error = 0,

        //++++For Login++++
        AccountAuthenicated = 1,
        AccountNotActivated = 2,
        AccountBanned = 3,
        AccountInvalid = 4,
        AccountActivated = 5,

        //++++For Register++++
        AccountCreated = 6,
        AccountNameUsed = 7,
        AccountEmailUsed = 8,

        ServerError = 255,
    }
}
