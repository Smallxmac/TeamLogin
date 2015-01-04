using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Network.Packets
{
    public class RegiesterRequest : PacketWriter
    {
        public RegiesterRequest(byte[] buffer) : base(buffer)
        {
        }

        public RegiesterRequest(short PacketLength, PacketTypes PacketType) : base(PacketLength, PacketType)
        {
            WriteHeader(PacketLength, PacketType);
        }

        private int _userNextOffset;
        private int _passNextOffset;
        public string Username
        {
            set { WriteStringWithLength(value, 4, out _userNextOffset);}
            get { return ReadStringFromLength(4, out _userNextOffset); }
        }

        public string Password
        {
            set { WriteStringWithLength(value, _userNextOffset, out _passNextOffset);}
            get { return ReadStringFromLength(_userNextOffset, out _passNextOffset);}
        }

        public string Email
        {
            set { WriteStringWithLength(value, _passNextOffset);}
            get { return ReadStringFromLength(_passNextOffset); }
        }
    }
}
