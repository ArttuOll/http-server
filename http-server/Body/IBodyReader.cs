namespace http_server.Body;

public interface IBodyReader
{
    public Task<MemoryStream> Read();
}