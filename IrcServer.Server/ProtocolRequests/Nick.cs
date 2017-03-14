using System.Linq;

namespace IrcServer.ProtocolRequests
{
    class Nick : IServerRequest
    {
        public void Run(User user, string request)
        {
            string nick = NickCheck(request);
            user.Nickname = nick;
            user.WriteLine($"INFO Your nick has been changed to {nick}").Wait();
            user.WriteLine($"NICK {nick}");
        }

        private string NickCheck(string nickname)
        {
            return nickname.Split(' ').First();
        }
    }
}
