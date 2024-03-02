using http_server.Request;
using HttpRequestException = http_server.Request.HttpRequestException;

namespace http_server_test.Request;

public class HttpRequestTests
{
    public HttpRequestTests()
    {
        Method = "GET";
        Uri = "/";
        Version = "HTTP/1.1";
        Headers = new Dictionary<string, string>
        {
            { "Host", "localhost:8080" },
            { "User-Agent", "Mozilla/5.0" }
        };

        Request = new HttpRequest(Method, Uri, Version, Headers);
    }

    private string Method { get; }
    private string Uri { get; }
    private string Version { get; }
    private Dictionary<string, string> Headers { get; }

    private HttpRequest Request { get; }

    [Fact]
    public void GetHeaderValue()
    {
        var host = Request.GetHeaderValue("Host");
        var userAgent = Request.GetHeaderValue("User-Agent");

        Assert.Equal("localhost:8080", host);
        Assert.Equal("Mozilla/5.0", userAgent);
    }

    [Fact]
    public void GetHeaderValue_Header_Not_Set()
    {
        Assert.Throws<HttpRequestException>(() => Request.GetHeaderValue("Content-Length"));
    }
}