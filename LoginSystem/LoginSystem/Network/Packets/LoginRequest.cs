using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Network.Packets
{
    class LoginRequest : PacketWriter
    {
        /// <summary>
        /// Constructor used for building a new login request packet.
        /// </summary>
        /// <param name="packetLength"></param>
        /// <param name="packetType"></param>
        public LoginRequest(short packetLength, PacketTypes packetType) : base(packetLength, packetType)
        {
            WriteHeader(packetLength, packetType);
        }

        /// <summary>
        /// Constructor for reading the byte array in the params.
        /// </summary>
        /// <param name="buffer">The byte array that is read from.</param>
        public LoginRequest(byte[] buffer)
            : base(buffer)
        {

        }

        private int _userNextOffset;
        private int _passNextOffset;

        /// <summary>
        /// Username value.
        /// </summary>
        public string Username
        {
            set { WriteStringWithLength(value, 4, out _userNextOffset); }
            get { return ReadStringFromLength(4, out _userNextOffset); }
        }
        /// <summary>
        /// Password value.
        /// </summary>
        public string Password
        {
            set { WriteStringWithLength(value, _userNextOffset, out _passNextOffset); }
            get { return ReadStringFromLength(_userNextOffset, out _passNextOffset); }
        }
        /// <summary>
        /// Email value.
        /// </summary>
        public string Email
        {
            set { WriteStringWithLength(value, _passNextOffset); }
            get { return ReadStringFromLength(_passNextOffset); }
        }

    }
}
