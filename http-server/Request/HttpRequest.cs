namespace http_server.Request;

public class HttpRequest(string method, string uri, string version, Dictionary<string, string> headers)
{
    public string Method { get; } = method;

    public string Uri { get; } = uri;

    public string Version { get; } = version;

    public Dictionary<string, string> Headers { get; } = headers;
}
