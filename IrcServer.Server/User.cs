using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IrcServer
{
    public class User
    {
        public TcpClient Client { get; private set; }

        private readonly StreamReader reader;
        private readonly StreamWriter writer;

        public User(TcpClient client)
        {
            Client = client;
            NetworkStream stream = Client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };

            // Say hello to client on connection
            if (Client.Connected)
            {
                WriteLine("INFO Connected to server");
            }
        }

        public Task<string> ReadLine()
        {
            return reader.ReadLineAsync();
        }

        public Task WriteLine(string value)
        {
            return writer.WriteLineAsync(value);
        }

        public void Disconnect(string message = null)
        {
            if (message == null)
            {
                message = "Connection reset by peer";
            }

            //TODO: Tell server it can't fire me, I quit!

            Client.Close();
        }
    }
}
