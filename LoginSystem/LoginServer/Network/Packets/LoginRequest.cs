using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Database;
using LoginServer.Enums;
using LoginServer.Objects;

namespace LoginServer.Network.Packets
{
    class LoginRequest : PacketWriter
    {
        /// <summary>
        /// Constructor for reading the byte array in the params.
        /// </summary>
        /// <param name="buffer">The byte array that is read from.</param>
        public LoginRequest(byte[] buffer) : base(buffer)
        {

        }

        private int _userNextOffset;
        private int _passNextOffset;

        /// <summary>
        /// Username value.
        /// </summary>
        public string Username
        {
            set { WriteStringWithLength(value, 4, out _userNextOffset); }
            get { return ReadStringFromLength(4, out _userNextOffset); }
        }
        /// <summary>
        /// Password value.
        /// </summary>
        public string Password
        {
            set { WriteStringWithLength(value, _userNextOffset, out _passNextOffset); }
            get { return ReadStringFromLength(_userNextOffset, out _passNextOffset); }
        }
        /// <summary>
        /// Email value.
        /// </summary>
        public string Email
        {
            set { WriteStringWithLength(value, _passNextOffset); }
            get { return ReadStringFromLength(_passNextOffset); }
        }
        /// <summary>
        /// The handler for the packet.
        /// </summary>
        /// <param name="request">The LoginRequest</param>
        /// <param name="passport">The client Passport</param>
        public void Handle(LoginRequest request, Passport passport)
        {
            string username = request.Username;
            string password = request.Password;
            string email = request.Email;
            bool usingemail = (username == "NONE");

            var accountHandler = new AccountHandler();

            

            var reply = new LoginResponse(18, PacketTypes.LoginResponse);
            if(usingemail)
                reply.LoginStatus = accountHandler.CheckAccountEmail(email, password, false);
            else
                reply.LoginStatus = accountHandler.CheckAccount(username, password, false);

            if (reply.LoginStatus == AccountStatus.AccountAuthenicated || reply.LoginStatus == AccountStatus.AccountNotActivated)
            {
                Account account = usingemail ? accountHandler.GetAccountEmail(email) : accountHandler.GetAccount(username);
                reply.Uid = account.UID;
                passport.Account = account;

                if (!Program.clients.ContainsKey(account.UID))
                    Program.clients.Add(account.UID, passport);
            }
            else if (reply.LoginStatus == AccountStatus.AccountBanned)
            {
                Account account = usingemail ? accountHandler.GetAccountEmail(email) : accountHandler.GetAccount(username);
                reply.BanDateExpire = account.AccountBanExpire;
            }
            passport.Send(reply.Build());

        }
    }
}
