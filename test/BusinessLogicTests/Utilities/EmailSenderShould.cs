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
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.Utilities;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Moq;
using NUnit.Framework;

namespace BusinessLogicTests.Utilities
{
    [TestFixture]
    public class EmailSenderShould
    {
        private Mock<SmtpClient> _smtpClientMock;
        private EmailSender _emailSender;

        [SetUp]
        public void SetUp()
        {
            _smtpClientMock = new Mock<SmtpClient>();
            _emailSender = new EmailSender(_smtpClientMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _emailSender.Dispose();
        }

        [Test]
        public void DoThrowExceptionWhenNoRecipient()
        {
            //Arrange
            var configuration = BuildTestConfiguration(new ToConfiguration());

            //Act/Assert
            var asyncAggregatedException = Assert.Throws<AggregateException>(() => _emailSender.SendEmailAsync(configuration).Wait());
            Assert.IsTrue(asyncAggregatedException.InnerExceptions.Count == 1);
            CollectionAssert.AllItemsAreInstancesOfType(asyncAggregatedException.InnerExceptions, typeof(InvalidOperationException));
        }


        [Test]
        public void DoSendToARecipient()
        {
            //Arrange
            MimeMessage resultMessage = null;
            var configuration = BuildTestConfiguration();
            _smtpClientMock.Setup(x => x.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>()))
                .Returns(Task.Factory.StartNew(() => { }))
                .Callback<MimeMessage, CancellationToken, ITransferProgress>((message, token, transferProgress) => resultMessage = message);

            //Act
            _emailSender.SendEmailAsync(configuration).Wait();

            //Assert
            Assert.AreEqual(configuration.To.EmailAddress, resultMessage.To.Mailboxes.First().Address);
        }

        [Test]
        public void DoSendToMultipleRecipients()
        {
            //Arrange
            MimeMessage resultMessage = null;
            var toConfiguration = new ToConfiguration
            {
                Subject = "test subject",
                Recipients =
                {
                    { "test@test.com", "Test User 1"},
                    { "test2@test.com", "Test User 2" },
                    { "test3@test.com", "Test User 3" }
                }
            };
            var configuration = BuildTestConfiguration(toConfiguration);
            _smtpClientMock.Setup(x => x.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>()))
                .Returns(Task.Factory.StartNew(() => { }))
                .Callback<MimeMessage, CancellationToken, ITransferProgress>((message, token, transferProgress) => resultMessage = message);

            //Act
            _emailSender.SendEmailAsync(configuration).Wait();

            //Assert
            Assert.AreEqual(toConfiguration.Recipients.Count, resultMessage.To.Mailboxes.Count());
            Assert.AreEqual("test@test.com", resultMessage.To.Mailboxes.First().Address);
            Assert.AreEqual("test2@test.com", resultMessage.To.Mailboxes.ToList()[1].Address);
        }

        [Test]
        public void DoSendToCarbonCopies()
        {
            //Arrange
            MimeMessage resultMessage = null;
            var toConfiguration = new ToConfiguration
            {
                Subject = "test subject",
                EmailAddress = "test@test.com",
                CarbonCopyRecipients =
                {
                    { "cc1@test.com", "Name1" }
                }
            };
            var configuration = BuildTestConfiguration(toConfiguration);
            _smtpClientMock.Setup(x => x.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>()))
                .Returns(Task.Factory.StartNew(() => { }))
                .Callback<MimeMessage, CancellationToken, ITransferProgress>((message, token, transferProgress) => resultMessage = message);

            //Act
            _emailSender.SendEmailAsync(configuration).Wait();

            //Assert
            Assert.AreEqual("cc1@test.com", resultMessage.Cc.Mailboxes.First().Address);
        }

        private EmailSenderConfiguration BuildTestConfiguration(ToConfiguration toConfiguration= null)
        {
            var configuration = new EmailSenderConfiguration();
            configuration.From.Name = "Somebody";
            configuration.From.EmailAddress = "some@email.com";
            configuration.SmtpServer = "smtp.test.com";
            configuration.SmtpPort = 25;
            configuration.Domain = "someDomain.com";
            configuration.EmailFormat = TextFormat.Text;
            configuration.SecureSocketOption = SecureSocketOptions.None;

            if (toConfiguration != null)
            {
                configuration.To = toConfiguration;
            }
            else
            {
                configuration.To.Subject = "Hello";
                configuration.To.EmailAddress = "someEmail@mail.com";
                configuration.To.Name = "some recipient";
            }

            configuration.EmailBody = "this is the body of the e-mail";

            return configuration;
        }

    }
}
