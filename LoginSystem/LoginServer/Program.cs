using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Database;
using LoginServer.Network;
using LoginServer.Network.Packets;
using LoginServer.Objects;

namespace LoginServer
{
    class Program
    {
        public static  Dictionary<int, Passport> clients; 
        public static Socket serverSocket;
        public static ConfigHandler Config;

        private static void Main(string[] args)
        {
            Config = new ConfigHandler(Properties.Settings.Default.ConnectionString);
            if (!Config.LoadConfig())
                Console.WriteLine("We seem to have a problem loading database settings "+ Config.LastError);

            clients = new Dictionary<int, Passport>();
            Console.WriteLine("Stating the sever...");
            ServerSocket loginSever = new ServerSocket();
            loginSever.StartServer(9950);
            while (true)
            {
                
            }

        }
    }
}
