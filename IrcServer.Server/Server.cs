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
                var user = new User(tcpClient);

                while (true)
                {
                    string command = await user.ReadLine();
                    Console.WriteLine($"Got {command}");
                    if (command != null)
                    {
                        // Handle command from client
                        HandleCommand(user, command);
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

        private void HandleCommand(User user, string message)
        {
            string[] parts = message.Split(' ');
            string instruction = parts[0];
            string response = "ERROR Unknown command";
            string data = null;

            // Check argument length
            if (parts.Length > 1)
            {
                data = string.Join(" ", parts, 1, instruction.Length - 2);
            }

            ICommand command = CommandRegistry.GetCommand(instruction);

            if (command != null)
            {
                var writer = new StreamWriter(user.Client.GetStream());
                writer.WriteLineAsync("Sending something");
                command.Run(user, data);
            }
            else
            {
                // Unknown command, inform client
                user.WriteLine(response);
            }
        }
    }
}
