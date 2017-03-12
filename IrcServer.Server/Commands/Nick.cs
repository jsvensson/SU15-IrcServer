using System.Linq;

namespace IrcServer.Commands
{
    class Nick : IServerCommand
    {
        public void Run(User user, string value)
        {
            string nick = NickCheck(value);
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
