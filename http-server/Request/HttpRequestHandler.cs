using System.Net;
using http_server.Body;
using http_server.Response;

namespace http_server.Request;

public class HttpRequestHandler(HttpRequest request, IBodyReader body)
{
    public HttpResponse Handle()
    {
        var headers = new Dictionary<string, string>
        {
            { "Content-Type", "text/plain" },
            { "Server", "http-server" }
        };

        return request.Uri switch
        {
            "/echo" => new HttpResponse(HttpStatusCode.OK, headers, body),
            _ => new HttpResponse(HttpStatusCode.OK, headers, null)
        };
    }
}