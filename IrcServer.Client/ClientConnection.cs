using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using IrcServer.Client.Commands.ServerRequest;

namespace IrcServer.Client
{
    public static class ClientConnection
    {
        private static TcpClient client;
        private static StreamReader reader;
        private static StreamWriter writer;

        public static void Connect(string ip, int port)
        {
            client = new TcpClient();
            client.Connect(ip, port);
            ClientMessage.Info("Connected.");

            NetworkStream stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) {AutoFlush = true};

            Task t = HandleConnection();
        }

        public static void Disconnect()
        {
            if (client.Connected)
            {
                client.Close();
                ClientMessage.Info("Connection closed.");
                return;
            }

            ClientMessage.Info("Not connected.");
        }

        private static async Task HandleConnection()
        {
            while (true)
            {
                string message = await reader.ReadLineAsync();

                if (message != null)
                {
                    HandleServerRequest(message);
                }
                else
                {
                    ClientMessage.Info("Lost connection to server");
                }
            }
        }

        private static void HandleServerRequest(string value)
        {
            value = value.Trim();
            string instruction = value;
            string args = null;

            // Check if we got parameters with the instruction
            if (value.IndexOf(' ') > -1)
            {
                instruction = value.SplitCommand()[0];
                args = value.SplitCommand()[1];
            }

            // Is request registered?
            IServerRequest request = ServerRequestRegistry.GetHandler(instruction);
            if (request != null)
            {
                request.Run(args);
            }
            else
            {
                // Protocol request unknown, inform client
                ClientMessage.Info($"Unknown protocol value: {value}");
            }
        }

        public static async Task WriteLine(string message)
        {
            if (client.Connected)
            {
                await writer.WriteLineAsync(message);
            }
            else
            {
                ClientMessage.Info("Not connected to server.");
            }
        }
    }
}
