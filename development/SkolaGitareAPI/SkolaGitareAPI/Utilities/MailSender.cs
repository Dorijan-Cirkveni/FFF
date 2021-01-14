using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkolaGitareAPI.Utilities
{
    public static class MailSender
    {

        public static async Task<Response> SendMail(string address, string subjectParameter, string body)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("dixaden724@28woman.com");
            var subject = subjectParameter;
            var to = new EmailAddress(address);
            var plainTextContent = "";
            var htmlContent = body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response =  await client.SendEmailAsync(msg);

            return response;
        }
    }
}
