using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Database;
using LoginServer.Objects;

namespace LoginServer.Network.Packets
{
    public class RegiesterRequest : PacketWriter
    {
        /// <summary>
        /// Constructor used to read the information on the byte array.
        /// </summary>
        /// <param name="buffer">The byte array</param>
        public RegiesterRequest(byte[] buffer)
            : base(buffer)
        {
        }

        /// <summary>
        /// Creates a new byte array for the packet.
        /// </summary>
        /// <param name="packetLength">The length of the packet.</param>
        /// <param name="packetType">The packet type.</param>
        public RegiesterRequest(short packetLength, PacketTypes packetType)
            : base(packetLength, packetType)
        {
            WriteHeader(packetLength, packetType);
        }

        private int _userNextOffset;
        private int _passNextOffset;

        /// <summary>
        /// The Username of the requested register.
        /// </summary>
        public string Username
        {
            set { WriteStringWithLength(value, 4, out _userNextOffset); }
            get { return ReadStringFromLength(4, out _userNextOffset); }
        }
        /// <summary>
        /// The password of the requested register.
        /// </summary>
        public string Password
        {
            set { WriteStringWithLength(value, _userNextOffset, out _passNextOffset); }
            get { return ReadStringFromLength(_userNextOffset, out _passNextOffset); }
        }
        /// <summary>
        /// The email of the requested register.
        /// </summary>
        public string Email
        {
            set { WriteStringWithLength(value, _passNextOffset); }
            get { return ReadStringFromLength(_passNextOffset); }
        }

        public void Handle(RegiesterRequest request, Passport passport)
        {
            var accountHandler = new AccountHandler();
            var reply = new RegisterResponse(6, PacketTypes.RegisterResponse);
            reply.RegisterStatus = accountHandler.RegisterAccount(request.Username, request.Password, request.Email);
            passport.clientSocket.Send(reply.Build(), 0, reply.BufferLength(), SocketFlags.None);
        }
    }
}
