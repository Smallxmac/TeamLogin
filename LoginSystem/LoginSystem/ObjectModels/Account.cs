using System;
using LoginSystem.Enums;

namespace LoginSystem.ObjectModels
{
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

        public bool Error = false;
    }
}
