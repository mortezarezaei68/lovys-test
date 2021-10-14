using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace EmailManagement.SendMessage
{
    public class SendingEmail:ISendingEmail
    {
        public EmailSettings _emailSettings { get; }

        public SendingEmail(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string bccEmail, string subject, string message)
        {
            Execute(_emailSettings.ToEmail, bccEmail,subject, message, null).Wait();

            return Task.FromResult(0);
        }
        public Task SendEmailWithToSenderAsync(string toEmail,string bccEmail, string subject, string message)
        {
            Execute(toEmail, bccEmail,subject, message, null).Wait();

            return Task.FromResult(0);
        }

        public Task SendEmailWithAttachmentsAsync(string bccEmail, string subject, string message, List<Attachment> attachments)
        {
            Execute(_emailSettings.ToEmail,bccEmail, subject, message, attachments).Wait();

            return Task.FromResult(0);
        }

        public async Task Execute(string email,string bccEmail, string subject, string message, List<Attachment> attachments)
        {
            try
            {
                var toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.FromAddress, _emailSettings.FromName)
                };

                mail.To.Add(new MailAddress(toEmail));

                // if (!string.IsNullOrEmpty(_emailSettings.CcEmail))
                //     mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                if (!string.IsNullOrEmpty(bccEmail))
                    mail.Bcc.Add(new MailAddress(bccEmail));

                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        mail.Attachments.Add(item);
                    }
                }

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.ServerAddress, _emailSettings.ServerPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    smtp.EnableSsl = _emailSettings.ServerUseSsl;

                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}