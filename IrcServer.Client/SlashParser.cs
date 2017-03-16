using IrcServer.Client.Commands.Slash;

namespace IrcServer.Client
{
    public static class SlashParser
    {
        public static void Parse(MainWindow window, string value)
        {
            value = value.Trim();
            string command = value.Substring(1);
            string args = null;

            // Check if command contains arguments
            if (value.IndexOf(' ') > -1)
            {
                command = value.Substring(1, value.IndexOf(' ') - 1);
                args = value.Substring(value.IndexOf(' ') + 1);
            }

            // Check if command exists
            ISlashCommand cmd = SlashCommandRegistry.GetHandler(command);

            if (cmd == null)
            {
                ClientMessage.Info($"Unknown command '{command}'");
                return;
            }

            cmd.Run(args);
        }
    }
}
