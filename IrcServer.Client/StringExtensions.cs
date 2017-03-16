namespace IrcServer.Client
{
    public static class StringExtensions
    {
        public static string[] SplitCommand(this string value)
        {
            var result = new string[2];
            result[0] = value.Substring(0, value.IndexOf(' '));
            result[1] = value.Substring(value.IndexOf(' ') + 1);

            return result;
        }

        public static bool IsChannel(this string value)
        {
            bool hasHash = value.IndexOf('#') == 0;
            bool noSpaces = value.IndexOf(' ') == -1;

            return hasHash && noSpaces;
        }
    }
}
