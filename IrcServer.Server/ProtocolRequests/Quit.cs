namespace IrcServer.ProtocolRequests
{
    class Quit : IServerRequest
    {
        public void Run(User user, string request)
        {
            if (request == null)
            {
                request = "Connection reset by peer";
            }

            user.Disconnect(request);
        }
    }
}
