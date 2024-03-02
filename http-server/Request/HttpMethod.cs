namespace http_server.Request;

public readonly struct HttpMethod
{
    public static string Get => "GET";
    public static string Head => "HEAD";
    public static string Post => "POST";
    public static string Put => "PUT";
    public static string Delete => "DELETE";
    public static string Connect => "CONNECT";
    public static string Options => "OPTIONS";
    public static string Trace => "TRACE";
    public static string Patch => "PATCH";
}