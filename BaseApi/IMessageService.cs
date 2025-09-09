namespace Zuhid.BaseApi;

public interface IMessageService
{
    Task<bool> SendEmail(string subject, string body, string to, string from = "noreply@company.com");
    Task<bool> SendSms(string phone, string message);
}