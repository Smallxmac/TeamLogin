using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Enums;

namespace LoginServer.Network.Packets
{
    public class LoginResponse : PacketWriter
    {
        /// <summary>
        /// Constructor for building the packet.
        /// </summary>
        /// <param name="packetLenght">Packet Length</param>
        /// <param name="packetType">Packet Type</param>
        public LoginResponse(short packetLenght, PacketTypes packetType) : base(packetLenght, packetType)
        {
            WriteHeader(packetLenght, packetType);
        }

        /// <summary>
        /// Account UID value. 
        /// </summary>
        public int Uid
        {
            set { WriteInt32(value, 4);}
            get { return ReadInt32(4); }
        }

        /// <summary>
        /// Account login status
        /// </summary>
        public AccountStatus LoginStatus
        {
            set { WriteInt16((short)value, 8); }
            get { return (AccountStatus)ReadInt16(8); }
        }

        /// <summary>
        /// Account BanExpire date
        /// </summary>
        public DateTime BanDateExpire
        {
            set { WriteInt64(value.ToFileTimeUtc(), 10); }
            get { return DateTime.FromFileTimeUtc(ReadInt64(10)); }
        }



    }
}
