using MaileSenderService;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ParseNamespace;
using MailEvents;


namespace mailservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailServiceController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly MailService _mailService;
        private readonly IEmailSender _emailSender;

        public MailServiceController(IPublishEndpoint publishEndpoint, MailService mailService, IEmailSender emailSender)
        {
            _publishEndpoint = publishEndpoint;
            _mailService = mailService;
            _emailSender = emailSender;
        }

        [HttpPost("store")]
        public async Task<IActionResult> StoreMailInfo([FromBody] MailInfo message)
        {

            if (message.Mail == null || message.AppKey == null)
            {
                return BadRequest("Mail and Appkey must not be empty");
                
            }

            await _mailService.StoreMailInfoInDb(message.Mail, message.AppKey);
            
            return Ok("Stored in db");
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMailTo([FromBody] MailMessage message)
        {

            if (message.Title == null || message.Receiver == null || message.data == null)
            {
                return BadRequest("Mail and Appkey must not be empty");
            }

            await _publishEndpoint.Publish(message); 
            return Ok("Message published successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetHealth()
        {
            return Ok("healthy");
        }
    }
}