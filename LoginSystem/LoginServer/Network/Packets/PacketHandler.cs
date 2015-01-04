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
        public static void Handle(byte[] buffer, Passport passport)
        {
            short PacketID = BitConverter.ToInt16(buffer, 0);
            short Length = BitConverter.ToInt16(buffer, 2);
            PacketTypes packet = (PacketTypes)Enum.ToObject(typeof(PacketTypes), PacketID);

            switch (packet)
            {
                case PacketTypes.LoginRequest:
                    var request = new LoginRequest(buffer);
                    request.Handle(request, passport);
                    break;

                case PacketTypes.RegisterRequest:
                    var registerRequest = new RegiesterRequest(buffer);
                    registerRequest.Handle(registerRequest, passport);
                    break;

                case PacketTypes.ValidationRequest:
                    var validateRequest = new ValidationRequest(buffer);
                    validateRequest.Handle(validateRequest, passport);
                    break;

                default:
                {
                    Console.WriteLine("Unhandled packet: " + packet + " with the length of: " + Length);
                    break;
                }
            }
        }
    }
}
