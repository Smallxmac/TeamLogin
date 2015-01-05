using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Objects;

namespace LoginServer.Network.Packets
{
    public static class PacketHandler
    {

        /// <summary>
        /// Main action used to handle the information received by the server.
        /// </summary>
        /// <param name="buffer">The byte array of information.</param>
        /// <param name="passport">The client passport.</param>
        public static void Handle(byte[] buffer, Passport passport)
        {
            short packetId = BitConverter.ToInt16(buffer, 0);
            short length = BitConverter.ToInt16(buffer, 2);
            var packet = (PacketTypes)Enum.ToObject(typeof(PacketTypes), packetId);

            switch (packet)
            {

                #region LoginRequest
                case PacketTypes.LoginRequest:
                    var request = new LoginRequest(buffer);
                    request.Handle(request, passport);
                    break;
                #endregion
                #region RegisterRequest
                case PacketTypes.RegisterRequest:
                    var registerRequest = new RegiesterRequest(buffer);
                    registerRequest.Handle(registerRequest, passport);
                    break;
                #endregion
                #region ValidationRequest
                case PacketTypes.ValidationRequest:
                    var validateRequest = new ValidationRequest(buffer);
                    validateRequest.Handle(validateRequest, passport);
                    break;
                #endregion

                default:
                {
                    Console.WriteLine("Unhandled packet: " + packet + " with the length of: " + length);
                    break;
                }
            }
        }
    }
}
