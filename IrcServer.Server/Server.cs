using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using IrcServer.Commands;

namespace IrcServer
{
    public class Server
    {
        private readonly int port;

        public Server(int port)
        {
            this.port = port;
            RegisterCommands();
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
                    if (command != null)
                    {
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
