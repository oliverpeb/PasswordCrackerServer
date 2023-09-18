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
        int FileNumber = 0;
        _listener.Start();
        Console.WriteLine("Server started on port " + Port);

        while (true)
        {
            Console.WriteLine("Waiting for a client...");
            var client = _listener.AcceptTcpClient();

            Console.WriteLine("Client connected!");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];

            using (var memoryStream = new System.IO.MemoryStream())
            {
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, bytesRead);

                }

                
                string FileName = "Received" + FileNumber + ".txt";
                FileNumber++;
                System.IO.File.WriteAllBytes(FileName, memoryStream.ToArray());
                Console.WriteLine($"File received and saved as {FileName}");
            }

            client.Close();
        }
    }

    public static void Main()
    {
        Server server = new Server();
        server.Start();
    }
}
