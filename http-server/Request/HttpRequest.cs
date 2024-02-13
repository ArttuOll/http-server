namespace http_server.Request;

public class HttpRequest(HttpMethod method, Uri uri, string version, Dictionary<string, string> headers)
{
    public HttpMethod Method { get; } = method;

    public Uri Uri { get; } = uri;

    public string Version { get; } = version;

    public Dictionary<string, string> Headers { get; } = headers;
}
