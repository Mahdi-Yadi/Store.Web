using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Emails
{
    public static class EmailSender
    {
        public static bool SendEmail(string EmailAddress, string Subject, string Body)
        {
            var result = false;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.example.com");
                mail.From = new MailAddress("test@example.com", "عنوان");
                mail.To.Add(EmailAddress);
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;
                SmtpServer.Credentials = new NetworkCredential("test@example.com", "123442345");
                SmtpServer.Send(mail);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}