﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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

            Task t = HandleConnection(client);
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

        private static async Task HandleConnection(TcpClient client)
        {
            while (true)
            {
                string message = await reader.ReadLineAsync();

                if (message != null)
                {
                    ClientMessage.Info($"Unhandled server message: {message}");
                }
                else
                {
                    ClientMessage.Info("Lost connection to server");
                }
            }
        }

        public static async Task WriteLine(string message)
        {
            if (client.Connected)
            {
                writer.WriteLineAsync(message).Wait();
            }
            else
            {
                ClientMessage.Info("Not connected to server.");
            }
        }


    }
}
