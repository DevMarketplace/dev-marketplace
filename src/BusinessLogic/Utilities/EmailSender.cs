#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2016 Tosho Toshev
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// GitHub repository: https://github.com/cracker4o/dev-marketplace
// e-mail: cracker4o@gmail.com
#endregion
using System;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace BusinessLogic.Utilities
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
