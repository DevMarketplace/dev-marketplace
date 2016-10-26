using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Facade
{
    public class EmailSender : IEmailSender, IDisposable
    {
        private readonly SmtpClient _smtpClient;

        public EmailSender()
        {
            _smtpClient = new SmtpClient();
        }

        internal EmailSender(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(EmailSenderConfiguration configuration)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(configuration.From.Name, configuration.From.EmailAddress));
            if (!SetRecipients(configuration, emailMessage))
            {
                throw new InvalidOperationException("There are no recipients set.");
            }

            SetCarbonCopy(configuration, emailMessage);

            emailMessage.Subject = configuration.To.Subject;
            emailMessage.Body = new TextPart(configuration.EmailFormat) { Text = configuration.EmailBody };
            _smtpClient.LocalDomain = configuration.Domain;
            await _smtpClient.ConnectAsync(configuration.SmtpServer, configuration.SmtpPort, configuration.SecureSocketOption).ConfigureAwait(false);
            await _smtpClient.SendAsync(emailMessage).ConfigureAwait(false);
            await _smtpClient.DisconnectAsync(true).ConfigureAwait(false);
        }

        private static void SetCarbonCopy(EmailSenderConfiguration configuration, MimeMessage emailMessage)
        {
            foreach (var ccRecipient in configuration.To.CarbonCopyRecipients)
            {
                emailMessage.Cc.Add(new MailboxAddress(ccRecipient.Value, ccRecipient.Key));
            }

            foreach (var bccRecipient in configuration.To.BlindCarbonCopyRecipients)
            {
                emailMessage.Bcc.Add(new MailboxAddress(bccRecipient.Value, bccRecipient.Key));
            }
        }

        private static bool SetRecipients(EmailSenderConfiguration configuration, MimeMessage emailMessage)
        {
            var hasRecipients = false;
            if (configuration.To.Recipients.Any())
            {
                foreach (var recipient in configuration.To.Recipients)
                {
                    emailMessage.To.Add(new MailboxAddress(recipient.Value, recipient.Key));
                }

                hasRecipients = true;
            }

            if (!string.IsNullOrWhiteSpace(configuration.To.EmailAddress))
            {
                emailMessage.To.Add(new MailboxAddress(configuration.To.Name, configuration.To.EmailAddress));
                hasRecipients = true;
            }

            return hasRecipients;
        }

        public void Dispose()
        {
            _smtpClient?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
