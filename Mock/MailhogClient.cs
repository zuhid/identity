using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class MailhogClient
{
    public static void Run()
    {
        var fromAddress = new MailAddress("test@example.com", "Test Sender");
        var toAddress = new MailAddress("you@example.com", "Test Receiver");
        const string subject = "Hello from MailHog";
        const string body = """
<h1>Hello</h1>
<ul>
    <li>one</li>
    <li>two</li>
    <li>three</li>
    <li>four</li>
</ul>
This is a test email sent via MailHog.
""";

        var smtp = new SmtpClient
        {
            Host = "localhost",
            Port = 1025,
            EnableSsl = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("username", "password") // Can be dummy
        };

        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        smtp.Send(message);
        Console.WriteLine("Email sent to MailHog!");
    }
}