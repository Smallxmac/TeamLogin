using System;
using LoginSystem.Enums;

namespace LoginSystem.ObjectModels
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
