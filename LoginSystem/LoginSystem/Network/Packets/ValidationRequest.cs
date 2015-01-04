using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Network.Packets
{
    public class ValidationRequest : PacketWriter
    {
        public ValidationRequest(byte[] buffer)
            : base(buffer)
        {
        }

        public ValidationRequest(short PacketLength, PacketTypes PacketType)
            : base(PacketLength, PacketType)
        {
            WriteHeader(PacketLength, PacketType);
        }

        public int UID
        {
            set { WriteInt32(value, 4); }
            get { return ReadInt32(4); }
        }
    }
}
