using System.Net;
using System.Net.Http.Headers;

namespace http_server.Response;

public class HttpResponse
{
    public HttpStatusCode StatusCode { get; }

    public HttpHeaders Headers { get; }

    public IBodyReader body { get; }
}

public interface IBodyReader
{
    public int Length { get; }

    public Task<byte[]> Read();
}