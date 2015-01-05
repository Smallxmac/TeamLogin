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
        private byte[] _buffer = new byte[550];

        /// <summary>
        /// Method used to start the server and listen on a port.
        /// </summary>
        /// <param name="port">the port number.</param>
        public void StartServer(int port)
        {
            try
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var ipEndPoint = new IPEndPoint(IPAddress.Any, port);
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

        /// <summary>
        /// Sends a byte array a client socket.
        /// </summary>
        /// <param name="buffer">the byte array</param>
        /// <param name="cleintSocket">the client socket.</param>
        public void SendtoClient(byte[] buffer, Socket cleintSocket)
        {
            if (cleintSocket.Connected)
                cleintSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallback, null);
        }

        /// <summary>
        /// The callback for the connection.
        /// </summary>
        /// <param name="ar">The AsyncResults</param>
        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);
                serverSocket.BeginAccept(AcceptCallback, null);
                _buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceviveCallback,
                    clientSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in connecting to the server. " + ex.Message);
            }
        }

        /// <summary>
        /// Callback that is called when data is received.
        /// </summary>
        /// <param name="ar">The AsyncResult</param>
        public void ReceviveCallback(IAsyncResult ar)
        {
            try
            {
                var clientSocket = (Socket) ar.AsyncState;
                short packetId = BitConverter.ToInt16(_buffer, 0);
                var packet = (PacketTypes)Enum.ToObject(typeof(PacketTypes), packetId);

                if (packet == PacketTypes.LoginRequest || packet == PacketTypes.RegisterRequest)
                {
                    var passport = new Passport {ClientSocket = clientSocket};
                    passport.Ip = ((IPEndPoint) passport.ClientSocket.RemoteEndPoint).Address.ToString();
                    PacketHandler.Handle(_buffer, passport);
                }
                else
                    PacketHandler.Handle(_buffer, Program.clients[BitConverter.ToInt32(_buffer, 4)]);
                
                clientSocket.EndReceive(ar);
                _buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceviveCallback, clientSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting the server " + ex.Message);
            }
        }

        /// <summary>
        /// The sending callback.
        /// </summary>
        /// <param name="ar">The AsyncResult</param>
        public void SendCallback(IAsyncResult ar)
        {
            var clientSocket = (Socket) ar.AsyncState;
            clientSocket.EndSend(ar);
        }
        
    }
}
