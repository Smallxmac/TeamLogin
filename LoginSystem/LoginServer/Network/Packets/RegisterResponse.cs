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
        public RegisterResponse(byte[] buffer) : base(buffer)
        {
        }

        public RegisterResponse(short PacketLength, PacketTypes PacketType) : base(PacketLength, PacketType)
        {
            WriteHeader(PacketLength, PacketType);
        }

        public AccountStatus RegisterStatus
        {
            set { WriteInt16((short)value, 4); }
            get { return (AccountStatus)ReadInt16(4); }
        }
    }
}
