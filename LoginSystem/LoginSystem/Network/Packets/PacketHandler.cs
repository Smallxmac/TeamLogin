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
        /// <summary>
        /// The Packet Handler that is called once the Socket is done
        /// receiving data from the sever.
        /// </summary>
        /// <param name="buffer">The Data received as a byte Array</param>
        public static void Handle(byte[] buffer)
        {
            short packetId = BitConverter.ToInt16(buffer, 0);
            short length = BitConverter.ToInt16(buffer, 2);
            var packet = (PacketTypes) Enum.ToObject(typeof (PacketTypes), packetId);

            switch (packet)
            {

                #region LoginResponse
                case PacketTypes.LoginResponse:
                    var reply = new LoginResponse(buffer);
                    reply.Handle(buffer);
                    break;
                #endregion
                #region RegisterResponse
                case PacketTypes.RegisterResponse:
                    var registerReply = new RegisterResponse(buffer);
                    registerReply.Handle(buffer);
                    break;
                #endregion

                default:
                    MessageBox.Show(null, @"Unhandled packet: " + packet + @" with the length of: " + length, @"Error",
                        MessageBoxButtons.OK);
                    break;
                
            }
        }
    }
}
