using MailKit.Net.Smtp;
using MimeKit;
namespace Reservation.API.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // prepare message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Reservation System", _configuration["Smtp:From"])); // from
            message.To.Add(MailboxAddress.Parse(toEmail)); // To
            message.Subject = subject; // subject

            var bodyy = new BodyBuilder { HtmlBody = body };
            message.Body = bodyy.ToMessageBody(); // body with corrected format

            // make connection on service provider
            using var client = new SmtpClient();
            await client.ConnectAsync(_configuration["Smtp:Host"] ,int.Parse( _configuration["Smtp:Port"]) , MailKit.Security.SecureSocketOptions.StartTls); // hey i wanna talk
            await client.AuthenticateAsync(_configuration["Smtp:Username"], _configuration["Smtp:Password"]); //this is my credintial 
            await client.SendAsync(message); 
            await client.DisconnectAsync(true);
        }
    }
}
