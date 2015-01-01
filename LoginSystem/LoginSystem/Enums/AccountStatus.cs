using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Enums
{ 
    /// <summary>
    /// AccountStatus is a set of enums which will be used in the login
    /// process to return information in a readable format.
    /// </summary>
    public enum AccountStatus
    {
        Error = 0,

        //++++For Login++++
        AccountAuthenicated = 1,
        AccountNotActivated = 2,
        AccountBanned = 3,
        AccountInvalid = 4,
        
        //++++For Register++++
        AccountCreated = 5,
        AccountNameUsed = 6,
        AccountEmailUsed = 7,

        ServerError = 255,
    }
}
