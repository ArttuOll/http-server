namespace http_server.Request;

public class HttpRequest(string method, string uri, string version, Dictionary<string, string> headers)
{
    public string Method { get; } = method;

    public string Uri { get; } = uri;

    public string Version { get; } = version;

    private Dictionary<string, string> Headers { get; } = headers;

    public string GetHeaderValue(string headerName)
    {
        try
        {
            return Headers[headerName];
        }
        catch (KeyNotFoundException error)
        {
            Console.WriteLine(error);
            throw new HttpRequestException(400, $"Necessary header '{headerName}' was not found in the request.");
        }
    }
}