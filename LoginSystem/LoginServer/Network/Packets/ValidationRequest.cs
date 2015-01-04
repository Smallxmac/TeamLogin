using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Database;
using LoginServer.IO;
using LoginServer.Objects;

namespace LoginServer.Network.Packets
{
    public class ValidationRequest : PacketWriter
    {
        public ValidationRequest(byte[] buffer) : base(buffer)
        {
        }

        public ValidationRequest(short PacketLength, PacketTypes PacketType) : base(PacketLength, PacketType)
        {
            WriteHeader(PacketLength, PacketType);
        }

        public int UID
        {
            set { WriteInt32(value, 4);}
            get { return ReadInt32(4); }
        }
        public void Handle(ValidationRequest validationRequest, Passport passport)
        {
            var accountHandler = new AccountHandler();
            var account = accountHandler.GetAccountId(UID);
            EmailSender.SendVerification(account);
        }
    }
}
