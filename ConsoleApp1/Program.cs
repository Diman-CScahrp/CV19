namespace OP
{
    public class RequestReceiverEventArgs : EventArgs 
    {
        public string Name { get; set; }
    }
    class Program
    {
        public static void Main()
        {
            Server server = new Server();
            server.RequestReceiver += Server_RequestReceiver;
        }

        private static void Server_RequestReceiver(object? sender, RequestReceiverEventArgs e)
        {
        }
    }
    class Server 
    {
        public event EventHandler<RequestReceiverEventArgs> RequestReceiver;
    }
}