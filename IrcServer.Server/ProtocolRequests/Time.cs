using System;

namespace IrcServer.ProtocolRequests
{
    public class Time : IServerRequest
    {
        public void Run(User user, string request)
        {
            string serverTime = DateTime.Now.ToLongTimeString();
            user.WriteLine($"SERVERTIME {serverTime}");
        }
    }
}
