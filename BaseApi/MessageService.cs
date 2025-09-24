using System.Net;
using System.Net.Mail;

namespace Zuhid.BaseApi;

public class MessageService : IMessageService {
  public async Task<bool> SendEmail(string subject, string body, string to, string from = "noreply@company.com") {
    var smtpClient = new SmtpClient {
      Host = "localhost",
      Port = 1025,
      Credentials = new NetworkCredential("username", "password"), // Can be dummy
      DeliveryMethod = SmtpDeliveryMethod.Network,
      EnableSsl = false,
      UseDefaultCredentials = false,
    };
    using var message = new MailMessage(from, to) {
      Subject = subject,
      Body = body,
      IsBodyHtml = true,
    };
    await smtpClient.SendMailAsync(message).ConfigureAwait(false);
    return true;
  }

  public async Task<bool> SendSms(string phone, string message) {
    return await SendEmail(phone, message, "phone@company.com", "phone@company.com").ConfigureAwait(false);
  }
}
