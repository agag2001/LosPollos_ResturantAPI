using LosPollos.Application.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Application.Services.Implementation
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration configuration;

        public EmailServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmail(string toEmail, string subject, string body)
        {
            int smtpPort = int.Parse(configuration["EmailSettings:Port"]);
            string serverName = configuration["EmailSettings:SmtpServer"];
            string senderEmail = configuration["EmailSettings:SenderEmail"];
            string senderPassword = configuration["EmailSettings:Password"];


            var email = new MimeMessage();
            email.From .Add( MailboxAddress.Parse( senderEmail));
            email.To.Add(MailboxAddress.Parse( toEmail));
            email.Subject = subject;
            email.Body = new TextPart("html")
            {
                Text = body     
            };

            var smtp = new SmtpClient();

            await  smtp.ConnectAsync(serverName, smtpPort,SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(senderEmail, senderPassword);     
            await smtp.SendAsync(email);   
            await smtp.DisconnectAsync(true);       
           
        }

    }
}
