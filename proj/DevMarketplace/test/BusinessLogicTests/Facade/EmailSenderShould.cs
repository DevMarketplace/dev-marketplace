using BusinessLogic.Facade;
using MailKit.Net.Smtp;
using Moq;
using NUnit.Framework;
using System;

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

        [Test]
        public void DoThrowExceptionWhenNoRecipient()
        {
            //Arrange
            var configuration = new EmailSenderConfiguration();

            //Act/Assert
            Assert.Throws<InvalidOperationException>(() => _emailSender.SendEmailAsync(configuration).Wait());
        }


        [Test]
        public void DoSendToARecipient()
        {
            //Arrange
            var configuration = new EmailSenderConfiguration();

            //Act
            _emailSender.SendEmailAsync(configuration).Wait();

            //Assert
            //_smtpClientMock.Object.
        }


    }
}
