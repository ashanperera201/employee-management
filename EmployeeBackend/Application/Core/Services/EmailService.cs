#region References
using Application.Core.Models.DTOs;
using Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
#endregion

#region Namespace
namespace Application.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfigDto _emailConfigs;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public EmailService(IOptions<EmailConfigDto> options)
        {
            _emailConfigs = options.Value;
        }

        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="content">The content.</param>
        /// <param name="subject">The subject.</param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(string email, string content, string subject)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sender", _emailConfigs.UserName));
                message.To.Add(new MailboxAddress("Recipient", email));
                message.Subject = subject;

                message.Body = new TextPart("html")
                {
                    Text = content
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(_emailConfigs.Provider, int.Parse(_emailConfigs.Port), false);
                    await client.AuthenticateAsync(_emailConfigs.UserName, _emailConfigs.Password);
                    await client.SendAsync(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
#endregion