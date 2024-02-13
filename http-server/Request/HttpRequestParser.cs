using System.Net;

namespace http_server.Request;

public static class HttpRequestParser
{
    public static HttpRequest ParseHeaderSection(List<string> headerSection)
    {
        try
        {
            var requestLine = headerSection[0].Split(' ');
            var method = ParseMethod(requestLine[0]);
            var uri = ParseUri(requestLine[1]);
            var version = ParseVersion(requestLine[2]);
            var headers = ParseHeaders(
                headerSection.GetRange(1, headerSection.Count - 1)
            );

            return new HttpRequest(method, uri, version, headers);
        }
        catch (Exception)
        {
            throw new HttpRequestException(400, "Error parsing the header section of the request.");
        }

    }

    private static HttpMethod ParseMethod(string method)
    {
        return new HttpMethod(method);
    }

    private static Uri ParseUri(string uri)
    {
        return new Uri(uri);
    }

    private static string ParseVersion(string version)
    {
        return version switch
        {
            "HTTP/1.0" => HttpVersion.Version10.ToString(),
            "HTTP/1.1" => HttpVersion.Version11.ToString(),
            "HTTP/2.0" => HttpVersion.Version20.ToString(),
            "HTTP/3.0" => HttpVersion.Version30.ToString(),
            _ => HttpVersion.Unknown.ToString()
        };
    }

    private static Dictionary<string, string> ParseHeaders(IEnumerable<string> headers)
    {
        return headers.Select(header => header.Split(": ")).ToDictionary(headerParts => headerParts[0], headerParts => headerParts[1]);
    }
}