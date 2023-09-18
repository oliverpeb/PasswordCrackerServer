using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    private TcpListener _listener;
    private const int Port = 12345;

    public Server()
    {
        _listener = new TcpListener(IPAddress.Any, Port);
    }

    public void Start()
    {
        _listener.Start();
        Console.WriteLine("Server started on port " + Port);

        while (true)
        {
            Console.WriteLine("Waiting for a client...");
            var client = _listener.AcceptTcpClient();

            Console.WriteLine("Client connected!");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];

            int bytesRead = stream.Read(buffer, 0, client.ReceiveBufferSize);

            string clientMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received message: " + clientMessage);

            client.Close();
        }
    }

    public static void Main()
    {
        Server server = new Server();
        server.Start();
    }
}
