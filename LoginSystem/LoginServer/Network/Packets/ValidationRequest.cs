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
        /// <summary>
        /// Constructor used to load the information.
        /// </summary>
        /// <param name="buffer">Byte Array of Information.</param>
        public ValidationRequest(byte[] buffer) : base(buffer)
        {
        }

        /// <summary>
        /// Constructor used to build a new packet. 
        /// </summary>
        /// <param name="packetLength">Packet Length</param>
        /// <param name="packetType">Packet Type</param>
        public ValidationRequest(short packetLength, PacketTypes packetType) : base(packetLength, packetType)
        {
            WriteHeader(packetLength, packetType);
        }

        /// <summary>
        /// The account Uid.
        /// </summary>
        public int Uid
        {
            set { WriteInt32(value, 4);}
            get { return ReadInt32(4); }
        }

        /// <summary>
        /// Handles the clients request to send the validation code of the account.
        /// </summary>
        /// <param name="validationRequest">An Instance of the information.</param>
        /// <param name="passport">The Clients Passport.</param>
        public void Handle(ValidationRequest validationRequest, Passport passport)
        {
            var accountHandler = new AccountHandler();
            var account = accountHandler.GetAccountId(Uid);
            EmailSender.SendVerification(account);
        }
    }
}
