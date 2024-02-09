using System.Net;
using http_server;

var ipAddress = IPAddress.Loopback;
const int port = 8080;

var server = new Server(ipAddress, port);
await server.Start();
