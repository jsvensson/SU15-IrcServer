using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IrcServer.Commands;

namespace IrcServer
{
    public class Server
    {
        private readonly IPAddress ipAddress;
        private readonly int port;

        public Server(int port)
        {
            RegisterCommands();

            this.port = port;

            // Hostname setup
            string hostName = Dns.GetHostName();
            IPHostEntry hostInfo = Dns.GetHostEntry(hostName);

            ipAddress = null;
            foreach (IPAddress t in hostInfo.AddressList)
            {
                if (t.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = t;
                    break;
                }
            }

            if (ipAddress == null)
            {
                throw new Exception("No IPv4 address detected");
            }
        }

        public void RegisterCommands()
        {
            Logger.Info("Registering protocol commands...");

            CommandRegistry.RegisterCommand("TIME", new Commands.Time());
        }

        public async void Start()
        {
            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Logger.Info("Server started");

            while (true)
            {
                try
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();

                    // Pass client along to protocol parser
                    Task t = HandleClient(tcpClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task HandleClient(TcpClient tcpClient)
        {
            string clientEndPoint = tcpClient.Client.RemoteEndPoint.ToString();
            Logger.Info($"New connection from {clientEndPoint}");

            try
            {
                NetworkStream stream = tcpClient.GetStream();
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream) {AutoFlush = true};

                // Say hello to client
                await writer.WriteLineAsync("INFO Client connected.");

                while (true)
                {
                    string command = await reader.ReadLineAsync();
                    if (command != null)
                    {
                        // Handle command from client
                        string response = HandleCommand(tcpClient, command);
                        if (response != null)
                        {
                            await writer.WriteLineAsync(response);
                        }
                    }
                    else
                    {
                        // Client closed connection
                        Logger.Info("Connection reset by peer");
                        tcpClient.Close();
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private string HandleCommand(TcpClient client, string message)
        {
            string[] verbs = message.Split(' ');
            string verb = verbs[0];
            string response = "ERROR Unknown command";
            string data = null;

            // Check argument length
            if (verbs.Length > 1)
            {
                data = string.Join(" ", verbs, 1, verb.Length - 2);
            }

            ICommand command;
            CommandRegistry.Commands.TryGetValue(verb, out command);

            if (command != null)
            {
                response = command.Run(data);
            }

            return response;
        }
    }
}
