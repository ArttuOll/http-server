namespace http_server.Request;

public class HttpRequestException(int statusCode, string message, Exception? innerException = null) : Exception(message,
    innerException)
{
    public int StatusCode { get; } = statusCode;
}