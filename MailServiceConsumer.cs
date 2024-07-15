using System.Numerics;
using MaileSenderService;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ParseNamespace;
using MailEvents;

public interface IEvent
  {
    DateTime Timestamp { get; }
    string CorrelationId { get; }
  }

public class MailConsumer : IConsumer<MailMessage>
{
    
    private readonly IEmailSender _emailSender;
    private readonly AppDbContext _context;

    public MailConsumer(IEmailSender emailSender, AppDbContext context)
    {
        _emailSender = emailSender;
        _context = context;
    }
    public async Task Consume(ConsumeContext<MailMessage> context)
    {
        var message = context.Message;
        Console.WriteLine($"Consumer > Received Mail message: {message.Receiver} | Title: {message.Title} | Data: {message.data}");
        
        // Step 1: Fetch the first MailInfo record from the database
        var mailInfo = await _context.MailInfos.FirstOrDefaultAsync();

        if (mailInfo == null)
        {
            throw new InvalidOperationException("No mail info found in the database.");
        }

        var response = await MailService.SendMail(
            message.Receiver,
            message.Title,
            message.data,
            _emailSender,
            mailInfo.Mail,
            mailInfo.AppKey
        );

        await Task.CompletedTask;

    }
}

namespace ParseNamespace{

    public class MailInfo
    {
        public required string Mail { get; set; }
        public required string AppKey { get; set; }
        
    }
}