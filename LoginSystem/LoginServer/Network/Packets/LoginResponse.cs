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
        public LoginResponse(short PacketLenght, PacketTypes PacketType) : base(PacketLenght, PacketType)
        {
            WriteHeader(PacketLenght, PacketType);
        }

        public int UID
        {
            set { WriteInt32(value, 4);}
            get { return ReadInt32(4); }
        }

        public AccountStatus LoginStatus
        {
            set { WriteInt16((short)value, 8); }
            get { return (AccountStatus)ReadInt16(8); }
        }

        public DateTime BaneDateExpire
        {
            set { WriteInt64(value.ToFileTimeUtc(), 10); }
            get { return DateTime.FromFileTimeUtc(ReadInt64(10)); }
        }



    }
}
