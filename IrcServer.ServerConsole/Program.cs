using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrcServer.ServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 23000;

            try
            {
                var server = new Server(port);
                server.Start();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
    }
}
