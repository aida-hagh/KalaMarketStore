using System;
using System.Net;
using System.Net.Mail;

public class sendEmail
    {
        public static bool Send(string to, string subject, string body)
        {
            try
            {
                MailMessage message = new MailMessage("aida1992.h@gmail.com", to);
                message.Body = body;
                message.Subject = subject;
                message.IsBodyHtml = true;
                NetworkCredential mailAuthentication = new NetworkCredential("aida1992.h@gmail.com", "0938787");
                SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
                mailClient.EnableSsl = true;
                mailClient.UseDefaultCredentials = false;
                mailClient.Credentials = mailAuthentication;
                mailClient.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }