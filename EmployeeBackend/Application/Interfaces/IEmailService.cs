namespace Application.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="content">The content.</param>
        /// <param name="subject">The subject.</param>
        /// <returns></returns>
        public Task<bool> SendEmailAsync(string email, string content, string subject);
    }
}
