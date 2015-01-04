using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Objects
{
    public class Passport
    {
        public string ip;
        public string ID;
        public Socket clientSocket;
        public Account account;
    }
}
