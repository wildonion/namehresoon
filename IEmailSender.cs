
namespace MaileSenderService
{
    public interface IEmailSender
    {
        void SendEmail(string receiver, string title, string data, string sender, string appkey);
    }
}