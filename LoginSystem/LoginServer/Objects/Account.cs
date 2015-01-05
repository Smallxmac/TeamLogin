using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Enums;

namespace LoginServer.Objects
{
    /// <summary>
    /// This is a masking object of the database table Accounts
    /// </summary>
    public class Account
    {
        public int UID;
        public string AccountUsername;
        public string AccountPassword;
        public string AccountEmail;
        public bool AccountActivated;
        public bool AccountBanned;
        public DateTime AccountBanExpire;
        public DateTime AccountRegisterDate;
        public AccountPermission AccountPermission;
        public string AccountActivationCode;

        public bool Error = false;
    }
}
