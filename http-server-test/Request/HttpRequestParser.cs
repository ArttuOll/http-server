using System.Net;
using http_server.Request;
using HttpMethod = http_server.Request.HttpMethod;

namespace http_server_test.Request;

public class HttpRequestParserTests
{
    [Fact]
    public void ParseHeaders()
    {
        var headers = new List<string>
        {
            "Host: localhost:8080",
            "User-Agent: Mozilla/5.0"
        };

        var parsedHeaders = HttpRequestParser.ParseHeaders(headers);

        Assert.Equal("localhost:8080", parsedHeaders["Host"]);
        Assert.Equal("Mozilla/5.0", parsedHeaders["User-Agent"]);
    }

    [Fact]
    public void ParseVersion()
    {
        var parsedVersion = HttpRequestParser.ParseVersion("HTTP/1.1");

        Assert.Equal(HttpVersion.Version11.ToString(), parsedVersion);
    }

    [Fact]
    public void ParseMethod()
    {
        var parsedGet = HttpRequestParser.ParseMethod("GET");
        var parsedPost = HttpRequestParser.ParseMethod("POST");
        var parsedPut = HttpRequestParser.ParseMethod("PUT");
        var parsedDelete = HttpRequestParser.ParseMethod("DELETE");

        Assert.Equal("GET", parsedGet);
        Assert.Equal("POST", parsedPost);
        Assert.Equal("PUT", parsedPut);
        Assert.Equal("DELETE", parsedDelete);
    }

    [Fact]
    public void ParseHeaderSection()
    {
        var headerSection = new List<string>
        {
            "GET / HTTP/1.1",
        };

        var parsedHeaderSection = HttpRequestParser.ParseHeaderSection(headerSection);

        Assert.Equal(HttpMethod.Get, parsedHeaderSection.Method);
        Assert.Equal(HttpVersion.Version11.ToString(), parsedHeaderSection.Version);
        Assert.Equal("/", parsedHeaderSection.Uri);
    }
}