using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Network.Packets
{
    public enum PacketTypes : short
    {
        LoginRequest = 8,
        RegisterRequest = 9,
        ValidationRequest = 10,
        LoginResponse = 12,
        RegisterResponse = 13,
        AccountInfo = 4,
        UserInfo = 5
    }
}
