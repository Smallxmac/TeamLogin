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
        public RegiesterRequest(byte[] buffer)
            : base(buffer)
        {
        }

        public RegiesterRequest(short PacketLength, PacketTypes PacketType)
            : base(PacketLength, PacketType)
        {
            WriteHeader(PacketLength, PacketType);
        }

        private int _userNextOffset;
        private int _passNextOffset;
        public string Username
        {
            set { WriteStringWithLength(value, 4, out _userNextOffset); }
            get { return ReadStringFromLength(4, out _userNextOffset); }
        }

        public string Password
        {
            set { WriteStringWithLength(value, _userNextOffset, out _passNextOffset); }
            get { return ReadStringFromLength(_userNextOffset, out _passNextOffset); }
        }

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
