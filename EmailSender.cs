using System.Net.Mail;
using System.Net;
using System.Text;
using System.Net.Security;
using MimeKit;
using MailKit.Security;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace MaileSenderService
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string receiver, string title, string data, string sender, string appkey)
        {
            
            try
            {
                Console.WriteLine($"Sending mail with master info: {sender} | key: {appkey}");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Aron", sender));
                message.To.Add(new MailboxAddress("", receiver));
                message.Subject = title;
                message.Body = new TextPart(TextFormat.Html) { Text = data };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate(sender, appkey);
                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.WriteLine($"mail was sent successfully");
            }
            catch (SmtpCommandException ex)
            {
                Console.WriteLine($"SMTP Command Error: {ex.Message}");
                Console.WriteLine($"Status Code: {ex.StatusCode}");
                throw;
            }
            catch (SmtpProtocolException ex)
            {
                Console.WriteLine($"SMTP Protocol Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw;
            }
        }

    }
}