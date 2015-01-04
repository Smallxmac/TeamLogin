using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LoginSystem.Network.Sockets;
using MySql.Data.MySqlClient.Memcached;

namespace LoginSystem.ObjectModels
{
    /// <summary>
    /// Simple passport to hold information that server sends us.
    /// </summary>
    public class Passport
    {
        public ClientSocket ClientSocket;
        public Account Account;
    }
}
