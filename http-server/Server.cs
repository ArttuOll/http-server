using System.Net;
using System.Net.Sockets;
using System.Text;

namespace http_server;

public class Server(IPAddress address, int port)
{
    private IPEndPoint IpEndpoint { get; } = new(address, port);

    private readonly TcpListener _listener = new(address, port);

    public async Task Start()
    {
        try
        {
            _listener.Start();
            Console.WriteLine($"Listening on {IpEndpoint}");

            while (true)
            {
                using var handler = await _listener.AcceptTcpClientAsync();
                await ServeClient(handler);
            }
        }
        finally
        {
            Stop();
        }
    }

    private async Task ServeClient(TcpClient client)
    {
        await using var stream = client.GetStream();
        using var streamReader = new StreamReader(stream, Encoding.UTF8);
        await using var streamWriter = new StreamWriter(stream, Encoding.UTF8);

        while (true)
        {
            var received = await streamReader.ReadLineAsync();

            Console.WriteLine($"Received message: {received}.\n", Encoding.UTF8);
        
            if (received == "quit")
            {
                client.Close();
                break;
            }
        
            var responseMessage = $"Echo: {received}\n";
        
            await streamWriter.WriteAsync(responseMessage);
            await streamWriter.FlushAsync();
        
            Console.WriteLine($"Sent message: {responseMessage}\n");
        }
    }

    private void Stop()
    {
        _listener.Stop();
    }
}