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
        private Socket cleintSocket;
        public ClientSocket(Socket socket)
        {
            cleintSocket = socket;
        }

        public void Connect(string ip, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            cleintSocket.BeginConnect(ipEndPoint, ConnectionCallback, null);
        }
        public void ConnectionCallback(IAsyncResult AR)
        {
            try
            {
                cleintSocket.EndConnect(AR);
                _buffer = new byte[cleintSocket.ReceiveBufferSize];
                cleintSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SendtoServer(byte[] buffer)
        {
            if(cleintSocket.Connected)
                cleintSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallback, null);
        }

        public void ReceiveCallback(IAsyncResult AR)
        {
            
            try
            {
                short PacketID = BitConverter.ToInt16(_buffer, 0);
                PacketTypes packet = (PacketTypes)Enum.ToObject(typeof(PacketTypes), PacketID);
                cleintSocket.EndReceive(AR);
                PacketHandler.Handle(_buffer);
                _buffer = new byte[cleintSocket.ReceiveBufferSize];
                cleintSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, null);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SendCallback(IAsyncResult AR)
        {
            try
            {
                cleintSocket.EndSend(AR);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
