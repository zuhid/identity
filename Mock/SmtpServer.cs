using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class SmtpServer
{
    public static void Run()
    {
        var tcpListener = new TcpListener(IPAddress.Any, 25);
        tcpListener.Start();
        Console.WriteLine("SMTP Server started on port 25...");

        while (true)
        {
            TcpClient client = tcpListener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            SendResponse(stream, "220 Simple SMTP Server Ready");

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string request = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received: " + request);

            // Basic command handling
            if (request.StartsWith("HELO"))
            {
                SendResponse(stream, "250 Hello");
            }

            // Add more command handling here...

            client.Close();
        }
    }

    static void SendResponse(NetworkStream stream, string message)
    {
        byte[] response = Encoding.ASCII.GetBytes(message + "\r\n");
        stream.Write(response, 0, response.Length);
    }
}

