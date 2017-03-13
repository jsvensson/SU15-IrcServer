using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IrcServer.Client
{
    public static class ClientConnection
    {
        private static TcpClient connection = new TcpClient();

        public static void Connect(string ip, int port)
        {
            connection = new TcpClient();
            connection.Connect(ip, port);
            ClientMessage.Info("Connected.");
        }

        public static void Disconnect()
        {
            if (connection.Connected)
            {
                ClientMessage.Info("Disconnecting...");
                connection.Close();
                ClientMessage.Info("Connection closed.");
                return;
            }

            ClientMessage.Info("Not connected.");
        }


    }
}
