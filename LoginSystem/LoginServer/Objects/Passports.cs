using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Objects
{
    /// <summary>
    /// Passport for each connecting client.
    /// </summary>
    public class Passport
    {
        public string Ip;
        public string Id;
        public Socket ClientSocket;
        public Account Account;

        public void Send(byte[] buffer)
        {
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
    }
}
