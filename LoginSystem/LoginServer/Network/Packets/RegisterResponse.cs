using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Enums;

namespace LoginServer.Network.Packets
{
    public class RegisterResponse : PacketWriter
    {
        /// <summary>
        /// The constructor that is used to read the byte array.
        /// </summary>
        /// <param name="buffer">The byte array</param>
        public RegisterResponse(byte[] buffer) : base(buffer)
        {
        }

        /// <summary>
        /// Constructor that is used to create a new packet.
        /// </summary>
        /// <param name="packetLength">The Packet Length</param>
        /// <param name="packetType">The Packet Type.</param>
        public RegisterResponse(short packetLength, PacketTypes packetType) : base(packetLength, packetType)
        {
            WriteHeader(packetLength, packetType);
        }

        /// <summary>
        /// The register status of the account.
        /// </summary>
        public AccountStatus RegisterStatus
        {
            set { WriteInt16((short)value, 4); }
            get { return (AccountStatus)ReadInt16(4); }
        }
    }
}
