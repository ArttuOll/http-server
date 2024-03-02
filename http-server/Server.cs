using System.Net;
using System.Net.Sockets;
using System.Text;
using http_server.Request;

namespace http_server;

public class Server(IPAddress address, int port)
{
    private readonly TcpListener _listener = new(address, port);
    private IPEndPoint IpEndpoint { get; } = new(address, port);

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

    private static async Task ServeClient(TcpClient client)
    {
        await using var stream = client.GetStream();
        using var streamReader = new StreamReader(stream, Encoding.UTF8);
        await using var streamWriter = new StreamWriter(stream, Encoding.UTF8);

        while (true)
        {
            var headerSection = await ReadHeader(streamReader);
            var request = HttpRequestParser.ParseHeaderSection(headerSection);

            // var responseMessage = $"Echo: {received}\n";

            // await streamWriter.WriteAsync(responseMessage);
            // await streamWriter.FlushAsync();

            // Console.WriteLine($"Sent message: {responseMessage}\n");
        }
    }

    private static async Task<List<string>> ReadHeader(TextReader streamReader)
    {
        var lines = new List<string>();
        var line = await streamReader.ReadLineAsync();
        while (!string.IsNullOrEmpty(line)) lines.Add(line);

        return lines;
    }

    private void Stop()
    {
        _listener.Stop();
    }
}