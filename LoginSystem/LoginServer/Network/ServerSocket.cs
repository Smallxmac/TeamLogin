using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LoginServer.Network.Packets;
using LoginServer.Objects;

namespace LoginServer
{
    public class ServerSocket
    {
        public Socket serverSocket;
        byte[] buffer = new byte[550];
        private int i = 0;

        public void StartServer(int port)
        {
            try
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(0);
                serverSocket.BeginAccept(AcceptCallback, null);
                Console.WriteLine("Server started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting the server "+ ex.Message);
            }
        }
        public void SendtoClient(byte[] buffer, Socket cleintSocket)
        {
            if (cleintSocket.Connected)
                cleintSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallback, null);
        }
        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                
                Socket clientSocket = serverSocket.EndAccept(ar);
                

                serverSocket.BeginAccept(AcceptCallback, null);

                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceviveCallback,
                    clientSocket);
                i++;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting the server " + ex.Message);
            }
        }

        public void ReceviveCallback(IAsyncResult ar)
        {
            try
            {
                
                Socket clientSocket = (Socket) ar.AsyncState;
                
                short PacketID = BitConverter.ToInt16(buffer, 0);
                PacketTypes packet = (PacketTypes)Enum.ToObject(typeof(PacketTypes), PacketID);
                if (packet == PacketTypes.LoginRequest || packet == PacketTypes.RegisterRequest)
                {
                    Passport passport = new Passport();
                    passport.clientSocket = clientSocket;
                    passport.ip = ((IPEndPoint) passport.clientSocket.RemoteEndPoint).Address.ToString();
                    PacketHandler.Handle(buffer, passport);
                }
                else
                {
                    PacketHandler.Handle(buffer, Program.clients[BitConverter.ToInt32(buffer, 4)]);
                }
                clientSocket.EndReceive(ar);
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceviveCallback, clientSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting the server " + ex.Message);
            }
        }

        public void SendCallback(IAsyncResult ar)
        {
            var clientSocket = (Socket) ar.AsyncState;
            clientSocket.EndSend(ar);
        }
        
    }
}
