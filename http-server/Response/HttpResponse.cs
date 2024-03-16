using System.Net;
using http_server.Body;

namespace http_server.Response;

public class HttpResponse(HttpStatusCode statusCode, Dictionary<string, string> headers, IBodyReader? body)
{
    public HttpStatusCode StatusCode { get; init; } = statusCode;

    public Dictionary<string, string> Headers { get; init; } = headers;

    public IBodyReader? Body { get; init; } = body;
}