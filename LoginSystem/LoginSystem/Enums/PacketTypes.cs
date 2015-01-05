using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Network.Packets
{

    /// <summary>
    /// The Packets that the server and client send to each other.
    /// Made into Enum to help with visualizing the Ids.
    /// </summary>
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
