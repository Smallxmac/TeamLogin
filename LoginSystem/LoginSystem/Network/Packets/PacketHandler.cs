using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginSystem.Network.Packets
{
    public static class PacketHandler
    {
        public static void Handle(byte[] buffer)
        {
            short PacketID = BitConverter.ToInt16(buffer, 0);
            short Length = BitConverter.ToInt16(buffer, 2);
            PacketTypes packet = (PacketTypes) Enum.ToObject(typeof (PacketTypes), PacketID);
            switch (packet)
            {
                case PacketTypes.LoginResponse:
                    var reply = new LoginResponse(buffer);
                    reply.Handle(buffer);
                    break;

                case PacketTypes.RegisterResponse:
                    var registerReply = new RegisterResponse(buffer);
                    registerReply.Handle(buffer);
                    break;

                default:
                    MessageBox.Show(null, "Unhandled packet: " + packet + " with the length of: " + Length, @"Error",
                        MessageBoxButtons.OK);
                    break;
                
            }
        }
    }
}
