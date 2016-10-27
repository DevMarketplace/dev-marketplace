using BusinessLogic.Facade;
using MailKit.Net.Smtp;
using Moq;
using NUnit.Framework;
using System;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System.Threading;
using MailKit;

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
            var configuration = BuildTestConfiguration();

            //Act
            _emailSender.SendEmailAsync(configuration).Wait();

            //Assert
            _smtpClientMock.Verify(x => x.SendAsync(It.IsAny<MimeMessage>(), It.IsAny<CancellationToken>(), It.IsAny<ITransferProgress>()));
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
