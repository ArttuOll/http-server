namespace http_server.Request;

public class HttpRequestException(int statusCode, string message) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}