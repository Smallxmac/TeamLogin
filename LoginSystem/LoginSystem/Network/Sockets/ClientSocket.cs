using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginSystem.Network.Packets;

namespace LoginSystem.Network.Sockets
{
    public class ClientSocket
    {
        private byte[] _buffer;
        private Socket clientSocket;

        /// <summary>
        /// Constructor for the Socket System.
        /// </summary>
        /// <param name="socket">The client socket that the user used to connect.</param>
        public ClientSocket(Socket socket)
        {
            clientSocket = socket;
        }

        /// <summary>
        /// Simple Connection Action. This will force the clientSocket to connect to the
        /// host that is in the params.
        /// </summary>
        /// <param name="ip">The IP of the target Host</param>
        /// <param name="port">The port of the target Host</param>
        public void Connect(string ip, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            clientSocket.BeginConnect(ipEndPoint, ConnectionCallback, null);
        }

        /// <summary>
        /// This callback is called once the clientSocket has connected to the server.
        /// </summary>
        /// <param name="ar">The AsyncResults</param>
        public void ConnectionCallback(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);
                _buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This is a little Action to help send data a bit faster.
        /// </summary>
        /// <param name="buffer">Byte Array that will be sent over the server.</param>
        public void SendtoServer(byte[] buffer)
        {
            if(clientSocket.Connected)
                clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallback, null);
        }

        /// <summary>
        /// Called once the clientSocket has Received some data.
        /// Once it has the data it will handle it then begin receiving the next amount of data.
        /// </summary>
        /// <param name="ar">The AsyncResults</param>
        public void ReceiveCallback(IAsyncResult ar)
        {
            
            try
            {
                clientSocket.EndReceive(ar);
                PacketHandler.Handle(_buffer);
                _buffer = new byte[clientSocket.ReceiveBufferSize];
                clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This is the callback that is called when the client is done sending data to the sever.
        /// </summary>
        /// <param name="ar">The AysncResults</param>
        public void SendCallback(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
