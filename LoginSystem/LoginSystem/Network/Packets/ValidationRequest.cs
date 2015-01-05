using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Network.Packets
{
    public class ValidationRequest : PacketWriter
    {
        /// <summary>
        /// Constructor used to load the information.
        /// </summary>
        /// <param name="buffer">Byte Array of Information.</param>
        public ValidationRequest(byte[] buffer)
            : base(buffer)
        {
        }

        /// <summary>
        /// Constructor used to build a new packet. 
        /// </summary>
        /// <param name="packetLength">Packet Length</param>
        /// <param name="packetType">Packet Type</param>
        public ValidationRequest(short packetLength, PacketTypes packetType)
            : base(packetLength, packetType)
        {
            WriteHeader(packetLength, packetType);
        }

        /// <summary>
        /// The account Uid.
        /// </summary>
        public int Uid
        {
            set { WriteInt32(value, 4); }
            get { return ReadInt32(4); }
        }
    }
}
