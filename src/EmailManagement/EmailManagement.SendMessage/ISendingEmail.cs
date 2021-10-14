using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailManagement.SendMessage
{
    public interface ISendingEmail
    {
        Task SendEmailAsync(string bccEmail, string subject, string message);
        Task SendEmailWithToSenderAsync(string toEmail, string bccEmail, string subject, string message);

        Task SendEmailWithAttachmentsAsync(string bccEmail, string subject, string message,
            List<Attachment> attachments);
    }
}