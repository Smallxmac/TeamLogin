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
        public LoginRequest(byte[] buffer) : base(buffer)
        {

        }

        private int User_next_offset;
        private int Pass_next_offset;
        public string Username
        {
            set { WriteStringWithLength(value, 4, out User_next_offset); }
            get { return ReadStringFromLength(4, out User_next_offset); }
        }

        public string Password
        {
            set { WriteStringWithLength(value, User_next_offset, out Pass_next_offset); }
            get { return ReadStringFromLength(User_next_offset, out Pass_next_offset); }
        }

        public string Email
        {
            set { WriteStringWithLength(value, Pass_next_offset); }
            get { return ReadStringFromLength(Pass_next_offset); }
        }

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
                reply.UID = account.UID;
                passport.account = account;

                if (!Program.clients.ContainsKey(account.UID))
                    Program.clients.Add(account.UID, passport);
            }
            else if (reply.LoginStatus == AccountStatus.AccountBanned)
            {
                Account account = usingemail ? accountHandler.GetAccountEmail(email) : accountHandler.GetAccount(username);
                reply.BaneDateExpire = account.AccountBanExpire;
            }
            passport.clientSocket.Send(reply.Build(), 0, reply.BufferLength(), SocketFlags.None);

        }
    }
}
