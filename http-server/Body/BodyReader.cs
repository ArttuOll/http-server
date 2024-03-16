using http_server.Request;
using HttpMethod = http_server.Request.HttpMethod;
using HttpRequestException = http_server.Request.HttpRequestException;

namespace http_server.Body;

public class BodyReader(Stream stream, HttpRequest request) : IBodyReader
{
    private Stream Stream { get; } = stream;
    private HttpRequest Request { get; } = request;

    public async Task<MemoryStream> Read()
    {
        var contentLength = ParseContentLength(request.GetHeaderValue("Content-Length"));

        var bodyNotAllowed = Request.Method == HttpMethod.Get || Request.Method == HttpMethod.Head;

        if (bodyNotAllowed && (contentLength > 0 || IsChunked()))
            throw new HttpRequestException(400, $"Request body not allowed for method: {request.Method}");

        if (contentLength > 0)
        {
            var buffer = new byte[contentLength];
            await Stream.ReadAsync(buffer.AsMemory(0, contentLength));
            return new MemoryStream(buffer);
        }

        if (IsChunked()) throw new HttpRequestException(501, "Chunked transfer encoding is not implemented yet.");

        // TODO: Read the rest of the connection
        throw new HttpRequestException(500, "TODO");
    }

    private static int ParseContentLength(string contentLength)
    {
        if (!int.TryParse(contentLength, out var length))
            throw new HttpRequestException(400, "Invalid Content-Length header value.");

        return length;
    }

    private bool IsChunked()
    {
        try
        {
            return Request.GetHeaderValue("Transfer-Encoding") == "chunked";
        }
        catch
        {
            return false;
        }
    }
}