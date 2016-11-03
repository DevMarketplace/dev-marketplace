using BusinessLogic.Facade;
using MailKit.Net.Smtp;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace BusinessLogicTests.Facade
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
