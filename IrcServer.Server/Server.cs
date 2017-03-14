using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using IrcServer.ProtocolRequests;

namespace IrcServer
{
    public class Server
    {
        private readonly int port;

        public List<User> Users { get; set; } = new List<User>();
        public static Dictionary<string, Channel> Channels { get; set; } = new Dictionary<string, Channel>();

        public Server(int port)
        {
            this.port = port;
            RegisterCommands();
        }

        public void RegisterCommands()
        {
            Logger.Info("Registering protocol commands...");

            CommandRegistry.RegisterCommand("TIME", new ProtocolRequests.Time());
            CommandRegistry.RegisterCommand("QUIT", new ProtocolRequests.Quit());
            CommandRegistry.RegisterCommand("JOIN", new ProtocolRequests.Join());
            CommandRegistry.RegisterCommand("PART", new ProtocolRequests.Part());
            CommandRegistry.RegisterCommand("PRIVMSG", new ProtocolRequests.PrivMsg());
            CommandRegistry.RegisterCommand("NICK", new ProtocolRequests.Nick());

            Logger.Info("Protocol commands registered.");
        }

        #region Network handling
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

            var user = new User(tcpClient);
            Users.Add(user);

            try
            {
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
                        user.Disconnect("Connection reset by peer");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                // TODO: Stop pretending the exception doesn't happen
                Logger.Info($"{e.GetType()}: {e.Message}");
            }
        }

        private void HandleCommand(User user, string value)
        {
            // Default return message
            string response = "ERROR Unknown command";

            value = value.Trim();
            string instruction = value;
            string args = null;

            // Check if we got parameters with the instruction
            if (value.IndexOf(' ') > -1)
            {
                instruction = value.SplitCommand()[0];
                args = value.SplitCommand()[1];
            }


            IServerRequest request = CommandRegistry.GetCommand(instruction);

            if (request != null)
            {
                request.Run(user, args);
            }
            else
            {
                // Unknown command, inform client
                user.WriteLine(response);
            }
        }
        #endregion Network handling

        #region IRC functionality
        public static Channel CreateChannel(string name)
        {
            var channel = new Channel(name);
            Channels.Add(channel.Name, channel);

            return channel;
        }
        #endregion IRC functionality
    }
}
