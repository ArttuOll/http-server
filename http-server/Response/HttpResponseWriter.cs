using System.Text;

namespace http_server.Response;

public class HttpResponseWriter(Stream stream, HttpResponse response)
{
    public async Task Write()
    {
        await using var streamWriter = new StreamWriter(stream, Encoding.UTF8);

        var responseMessage = EncodeResponse(response);
        await streamWriter.WriteAsync(responseMessage);

        if (response.Body != null)
        {
            var body = await response.Body.Read();
            await stream.WriteAsync(body.ToArray());
        }

        await streamWriter.FlushAsync();
    }

    private static string EncodeResponse(HttpResponse response)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"HTTP/1.1 {response.StatusCode} OK\n");

        foreach (var (key, value) in response.Headers) stringBuilder.Append($"{key}: {value}\n");
        stringBuilder.Append('\n');

        return stringBuilder.ToString();
    }
}