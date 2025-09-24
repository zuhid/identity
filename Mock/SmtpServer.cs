using System.Net;
using System.Net.Sockets;
using System.Text;

internal class SmtpServer {
  public static void Run() {
    var tcpListener = new TcpListener(IPAddress.Any, 25);
    tcpListener.Start();
    Console.WriteLine("SMTP Server started on port 25...");

    while (true) {
      var client = tcpListener.AcceptTcpClient();
      var stream = client.GetStream();

      SendResponse(stream, "220 Simple SMTP Server Ready");

      var buffer = new byte[1024];
      var bytesRead = stream.Read(buffer, 0, buffer.Length);
      var request = Encoding.ASCII.GetString(buffer, 0, bytesRead);
      Console.WriteLine("Received: " + request);

      // Basic command handling
      if (request.StartsWith("HELO")) {
        SendResponse(stream, "250 Hello");
      }

      // Add more command handling here...

      client.Close();
    }
  }

  private static void SendResponse(NetworkStream stream, string message) {
    var response = Encoding.ASCII.GetBytes(message + "\r\n");
    stream.Write(response, 0, response.Length);
  }
}
