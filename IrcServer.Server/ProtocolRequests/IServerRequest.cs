namespace IrcServer.ProtocolRequests
{
    public interface IServerRequest
    {
        void Run(User user, string request);
    }
}