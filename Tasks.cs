


// 'async void' can also be used for specific use cases, such as events:
// public async void SendVerifyButton_Click(object sender, EventArgs e)  
using System.Numerics;
using System.Text;
using MaileSenderService;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;

public class MailService
{
    private readonly AppDbContext _context;

    public MailService(AppDbContext context)
    {
        _context = context;
    }

    public static async Task<string> SendMail(string receiver, string title, string data, IEmailSender _emailSender, string sender, string appkey)
    {
    
        
        _emailSender.SendEmail(receiver, title, data, sender, appkey);

        return "Mail sent successfully.";
    }

    public async Task StoreMailInfoInDb(string mail, string appKey)
    {
        var existingMailInfo = await _context.MailInfos.SingleOrDefaultAsync(mi => mi.Mail == mail);

        if (existingMailInfo != null)
        {
            // Update existing entry
            existingMailInfo.AppKey = appKey;
            _context.MailInfos.Update(existingMailInfo);
        }
        else
        {
            // Insert new entry
            var mailInfo = new MailInfo
            {
                Mail = mail,
                AppKey = appKey
            };
            _context.MailInfos.Add(mailInfo);
        }

        await _context.SaveChangesAsync();
    }

}