using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;

namespace AcologAPI.src.Application.Services
{
    public class EmailService : ISendEmail
    {
        static string[] Scopes = { GmailService.Scope.GmailSend, GmailService.Scope.GmailReadonly};
        private GmailService _service;
        private string emailFrom = DotNetEnv.Env.GetString("EMAIL_PADRAO");
        private string userFrom = DotNetEnv.Env.GetString("EMAIL_USER");

        public EmailService(string credentialPath, string tokenPath)
        {
            using var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read);
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(
                    tokenPath, true
                )
            ).Result;

            if(credential.Token.IsExpired(credential.Flow.Clock))
            {
                credential.RefreshTokenAsync(CancellationToken.None);
            }

            _service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Gmail APi .NEt"
            });
        }

        public void SendEmail(string to, string subject, string body)
        {
            var emailMessage = new MimeMessage();

            //User / email
            emailMessage.From.Add(new MailboxAddress(userFrom, emailFrom));
            emailMessage.To.Add(new MailboxAddress("" ,to));
            emailMessage.Subject = subject;

            var BodyBuilder = new BodyBuilder();

            BodyBuilder.HtmlBody = string.Format($"{body}");

            emailMessage.Body = BodyBuilder.ToMessageBody();

            using var memoryStream = new MemoryStream();
            emailMessage.WriteTo(memoryStream);

            var rawMessage = Convert.ToBase64String(memoryStream.ToArray())
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");

            var message = new Message
            {
                Raw = rawMessage,
            };

            _service.Users.Messages.Send(message, "me").Execute();
        }

        public void ReadEmails()
        {
            var req = _service.Users.Messages.List("me");
            req.LabelIds = "INBOX";
            req.IncludeSpamTrash = false;

            var res = req.Execute();

            if(res != null && res.Messages != null)
            {
                    foreach(var items in res.Messages)
                    {
                        var message = _service.Users.Messages.Get("me", items.Id).Execute();
                        var subjectHeader = message.Payload.Headers.FirstOrDefault(h => h.Name == "Subject");

                        Console.WriteLine($"Subject: {subjectHeader?.Value}");
                    }
            }

        }

    }
}